﻿/* MIT License
Copyright (c) 2011-2019 Markus Wendt (http://www.dodoni-project.net)

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 
Please see http://www.dodoni-project.net/ for more information concerning the Dodoni.net project. 
*/
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Dodoni.Finance;
using Dodoni.BasicComponents;
using Dodoni.BasicComponents.Utilities;

namespace Dodoni.Finance.CommonMarketUsages.HolidayCalendars
{
    /// <summary>Represents a holiday calendar where the business days are defined by the <c>union</c> of the business days of several holiday calendars.
    /// </summary>
    internal class UnionBusinessDaysJoinHolidayCalendar : JointHolidayCalendar
    {
        #region public constructors

        /// <summary>Initializes a new instance of the <see cref="UnionBusinessDaysJoinHolidayCalendar"/> class.
        /// </summary>
        /// <param name="calendarName">The name of the calendar.</param>
        /// <param name="holidayCalendarCollection">A collection of <see cref="IHolidayCalendar"/> instances.</param>
        /// <exception cref="ArgumentNullException">Thrown, if one of the arguments is <c>null</c>.</exception>
        public UnionBusinessDaysJoinHolidayCalendar(IdentifierString calendarName, IEnumerable<IHolidayCalendar> holidayCalendarCollection)
            : base(calendarName, holidayCalendarCollection, GetWeekendRepresentation(holidayCalendarCollection))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="UnionBusinessDaysJoinHolidayCalendar"/> class.
        /// </summary>
        /// <param name="calendarName">The name of the calendar.</param>
        /// <param name="holidayCalendar1">The first holiday calendar.</param>
        /// <param name="holidayCalendar2">The second holiday calendar.</param>
        /// <exception cref="ArgumentNullException">Thrown, if one of the arguments is <c>null</c>.</exception>
        public UnionBusinessDaysJoinHolidayCalendar(IdentifierString calendarName, IHolidayCalendar holidayCalendar1, IHolidayCalendar holidayCalendar2)
            : base(calendarName, holidayCalendar1, holidayCalendar2, GetWeekendRepresentation(holidayCalendar1, holidayCalendar2))
        {
        }
        #endregion

        #region public methods

        #region IHolidayCalendar Members

        #region info methods

        /// <summary>Gets the holiday table for a specific year as a collection of <see cref="DateInfo"/> objects, where in general the language depending name of the holiday is given too.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="holidaySet">A set of <see cref="DateInfo"/> object that represents the holidays (output).</param>
        /// <param name="holidayType">The type of the holidays to take into account.</param>
        /// <exception cref="NullReferenceException">Thrown, if <paramref name="holidaySet"/> is <c>null</c>.</exception>
        public override void GetHolidays(int year, HashSet<DateInfo> holidaySet, HolidayType holidayType = HolidayType.PublicHolidays)
        {
            if (holidaySet == null)
            {
                throw new ArgumentNullException("holidaySet");
            }

            /* the holidays (i.e. public holidays as well as weekend days) are the intersection of the holidays: */
            HashSet<DateInfo> intersectionHolidays = new HashSet<DateInfo>();
            m_HolidayCalendars.First<IHolidayCalendar>().GetHolidays(year, intersectionHolidays, holidayType);

            HashSet<DateInfo> oneStepHolidays = new HashSet<DateInfo>();
            foreach (IHolidayCalendar holidayCalendar in m_HolidayCalendars)
            {
                holidayCalendar.GetHolidays(year, oneStepHolidays, holidayType);
                intersectionHolidays.IntersectWith(oneStepHolidays);
                oneStepHolidays.Clear();
            }
            holidaySet.UnionWith(intersectionHolidays);
        }

        /// <summary>Gets the public holidays for a specific year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="holidaySet">A set of public holidays with respect to <paramref name="year"/>; dates that represents a weekend (i.e. Saturday's and sunday's for the 'standard' weekend) 
        /// will not be added, given elements remain unchanged (output).</param>
        /// <exception cref="NullReferenceException">Thrown, if <paramref name="holidaySet"/> is <c>null</c>.</exception>
        public override void GetHolidays(int year, HashSet<DateTime> holidaySet)
        {
            /* the interception of the holidays: */
            HashSet<DateTime> intersectionHolidays = new HashSet<DateTime>();
            m_HolidayCalendars.First<IHolidayCalendar>().GetHolidays(year, intersectionHolidays);  // holidays of the first holiday calendar

            HashSet<DateTime> oneStepHolidays = new HashSet<DateTime>();
            foreach (IHolidayCalendar holidayCalendar in m_HolidayCalendars)
            {
                holidayCalendar.GetHolidays(year, oneStepHolidays);
                intersectionHolidays.IntersectWith(oneStepHolidays);
                oneStepHolidays.Clear();
            }
            holidaySet.UnionWith(intersectionHolidays);
        }
        #endregion

        /// <summary>Determines whether some specified date is a business day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns><c>true</c> if <paramref name="date"/> is a business day with respect to the current instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown, if for the <paramref name="date"/> the current
        /// calendar is not defined, i.e. <paramref name="date"/> is less than <see cref="IHolidayCalendar.FirstDate"/>
        /// or greater than <see cref="IHolidayCalendar.LastDate"/>.</exception>
        public override bool IsBusinessDay(DateTime date)
        {
            if ((date < FirstDate) || (date > LastDate))
            {
                throw new ArgumentException("'IsBusinessDay' fails for date " + date.ToString("d") + " because the holiday calendar " + Name.String + " is not defined for year " + date.Year.ToString() + ".", "date");
            }
            foreach (IHolidayCalendar holidayCalendar in m_HolidayCalendars)
            {
                if (holidayCalendar.IsBusinessDay(date))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #endregion

        #region protected methods

        /// <summary>Determines whether some specified date is a business day but do not check whether the given date is inside
        /// <see cref="IHolidayCalendar.FirstDate"/> and <see cref="IHolidayCalendar.LastDate"/>.
        /// </summary>
        /// <param name="nonWeekendDay">The date which is neither a Saturday nor a Sunday.</param>
        /// <returns><c>true</c> if <paramref name="nonWeekendDay"/> is a business day with respect to the current instance; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsBusinessDayForNonWeekendDay(DateTime nonWeekendDay)
        {
            foreach (IHolidayCalendar holidayCalendar in m_HolidayCalendars)
            {
                if (holidayCalendar.IsBusinessDay(nonWeekendDay))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Gets the annotation, i.e. builds the description of the current holiday calendar.
        /// </summary>
        /// <returns>The annotation, i.e. the description of the current holiday calendar that contains the name of each sub holiday calendar.
        /// </returns>
        protected override string GetAnnotation()
        {
            StringBuilder listOfCalendarNames = new StringBuilder();
            foreach (IHolidayCalendar calendar in m_HolidayCalendars)
            {
                string subName = calendar.Name.String;
                if (String.IsNullOrEmpty(subName) == false)
                {
                    if (listOfCalendarNames.Length > 0)
                    {
                        listOfCalendarNames.Append(", ");
                    }
                    listOfCalendarNames.Append(subName);
                }
            }
            return String.Format(Resources.JointBusinessCalendarDescription, listOfCalendarNames.ToString());
        }
        #endregion

        #region private static methods

        /// <summary>Gets the weekend representation of a set of <see cref="IHolidayCalendar"/> objects.
        /// </summary>
        /// <param name="holidayCalendarCollection">The holiday calendar list.</param>
        /// <returns>A <see cref="IWeekendRepresentation"/> object which is the intersection of the <see cref="IWeekendRepresentation"/>
        /// objects of the <paramref name="holidayCalendarCollection"/>.</returns>
        private static IWeekendRepresentation GetWeekendRepresentation(IEnumerable<IHolidayCalendar> holidayCalendarCollection)
        {
            ISet<DayOfWeek> weekendDays = new HashSet<DayOfWeek>();
            holidayCalendarCollection.First().WeekendRepresentation.AddWeekendDaysTo(weekendDays);
            foreach (IHolidayCalendar holidayCalendar in holidayCalendarCollection.Skip(1))
            {
                holidayCalendar.WeekendRepresentation.IntersectWeekendDaysWith(weekendDays);
            }
            return WeekendFactory.GetWeekend(weekendDays);
        }

        /// <summary>Gets the weekend representation with respect to two <see cref="IHolidayCalendar"/> objects.
        /// </summary>
        /// <param name="holidayCalendar1">The first holiday calendar.</param>
        /// <param name="holidayCalendar2">The second holiday calendar.</param>
        /// <returns>A <see cref="IWeekendRepresentation"/> object which is the intersection of the <see cref="IWeekendRepresentation"/>
        /// objects of <paramref name="holidayCalendar1"/> and <paramref name="holidayCalendar2"/>.</returns>
        private static IWeekendRepresentation GetWeekendRepresentation(IHolidayCalendar holidayCalendar1, IHolidayCalendar holidayCalendar2)
        {
            ISet<DayOfWeek> weekendDays = new HashSet<DayOfWeek>();
            holidayCalendar1.WeekendRepresentation.AddWeekendDaysTo(weekendDays);
            holidayCalendar2.WeekendRepresentation.IntersectWeekendDaysWith(weekendDays);
            return WeekendFactory.GetWeekend(weekendDays);
        }
        #endregion
    }
}