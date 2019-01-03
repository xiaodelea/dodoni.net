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
using System.Text;
using System.Collections.Generic;

using Dodoni.Finance;
using Dodoni.BasicComponents;

namespace Dodoni.Finance.CommonMarketUsages.BusinessDayConventions
{
    /// <summary>The business day convention 'Two business days prior third wednesday adjustment', i.e. dates are adjusted to two business day prior the third wednesday of the month.
    /// </summary>
    internal class TwoBusinessDaysPriorThirdWednesdayAdjustment : IBusinessDayConvention
    {
        #region private static readonly members

        /// <summary>The name of the business day convention.
        /// </summary>
        private static readonly IdentifierString sm_Name = new IdentifierString("Two business days prior third wednesday");

        /// <summary>The long name of the business day convention, i.e. language dependent.
        /// </summary>
        private static readonly IdentifierString sm_LongName = new IdentifierString(BusinessDayConventionResources.TwoBusinessDaysPriorThirdWednesdayLongName);

        /// <summary>The annotation, i.e. description of the business day convention.
        /// </summary>
        private static readonly string sm_Annotation = BusinessDayConventionResources.TwoBusinessDaysPriorThirdWednesday;
        #endregion

        #region public constructors

        /// <summary>Initializes a new instance of the <see cref="TwoBusinessDaysPriorThirdWednesdayAdjustment"/> class.
        /// </summary>
        public TwoBusinessDaysPriorThirdWednesdayAdjustment()
        {
        }
        #endregion

        #region public properties

        #region IBusinessDayConvention Members

        /// <summary>Gets the type of the adjustment, i.e. a value indicating whether the result of <see cref="IBusinessDayConvention.GetAdjustedDate(DateTime, IHolidayCalendar)"/> is a business day.
        /// </summary>
        /// <value>The type of the date adjustment.</value>
        public BusinessDayAdjustmentType AdjustmentType
        {
            get { return BusinessDayAdjustmentType.AdjustmentToBusinessDay; }
        }
        #endregion

        #region IIdentifierNameable Members

        /// <summary>Gets the name of the business day convention.
        /// </summary>
        /// <value>The name of the business day convention.</value>
        public IdentifierString Name
        {
            get { return TwoBusinessDaysPriorThirdWednesdayAdjustment.sm_Name; }
        }

        /// <summary>Gets the long name of the business day convention.
        /// </summary>
        /// <value>The language dependent long name of the business day convention.</value>
        public IdentifierString LongName
        {
            get { return TwoBusinessDaysPriorThirdWednesdayAdjustment.sm_LongName; }
        }
        #endregion

        #region IAnnotatable Members

        /// <summary>Gets a value indicating whether the annotation is readonly.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the annotation of this instance is readonly; otherwise, <c>false</c>.
        /// </value>
        bool IAnnotatable.HasReadOnlyAnnotation
        {
            get { return true; }
        }

        /// <summary>Gets the annotation of the current instance.
        /// </summary>
        /// <value>The annotation of the current instance.</value>
        public string Annotation
        {
            get { return TwoBusinessDaysPriorThirdWednesdayAdjustment.sm_Annotation; }
        }
        #endregion

        #endregion

        #region public methods

        #region IAnnotatable Members

        /// <summary>Sets the annotation of the current instance.
        /// </summary>
        /// <param name="annotation">The annotation.</param>
        /// <returns>A value indicating whether the <see cref="Annotation"/> has been changed.
        /// </returns>
        bool IAnnotatable.TrySetAnnotation(string annotation)
        {
            return false;
        }
        #endregion

        #region IBusinessDayConvention Members

        /// <summary>Gets an adjusted date with respect to a specific <see cref="System.DateTime"/> object.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="holidayCalendar">The holiday calendar.</param>
        /// <returns>The <see cref="System.DateTime"/> object that is given by <paramref name="date"/> taken into account the business day
        /// convention represented by the current instance.
        /// </returns>
        /// <remarks>Perhaps the return value is not a business day, for example in the case of some end-of-month adjustment or 'no adjustment'.</remarks>
        public DateTime GetAdjustedDate(DateTime date, IHolidayCalendar holidayCalendar)
        {
            DateTime FirstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            int wednesdayIndex = ((10 - ((int)FirstDayOfMonth.DayOfWeek)) % 7 + 1) + 14;

            /* if the wednesday is a business day, we go two business days back, otherwise one
             * business day. In general in both cases the 'monday' will be returned: */

            DateTime previousBusinessDay = holidayCalendar.GetPreviousAdjustedBusinessDay(new DateTime(date.Year, date.Month, wednesdayIndex));
            if (previousBusinessDay.DayOfWeek == DayOfWeek.Wednesday)  // wednesday is some business day
            {
                return holidayCalendar.AddBusinessDays(previousBusinessDay, -2);
            }
            return holidayCalendar.AddBusinessDays(previousBusinessDay, -1);
        }
        #endregion

        /// <summary>Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return sm_LongName.String;
        }
        #endregion
    }
}