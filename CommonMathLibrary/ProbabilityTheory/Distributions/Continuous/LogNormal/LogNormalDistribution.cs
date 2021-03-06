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
using System.Numerics;

using Dodoni.BasicComponents;
using Dodoni.MathLibrary.Basics;
using Dodoni.BasicComponents.Containers;

namespace Dodoni.MathLibrary.ProbabilityTheory.Distributions
{
    /// <summary>Provides methods for cumulative distribution function etc. with respect to the Log-Normal distribution.
    /// </summary>
    public partial class LogNormalDistribution : IProbabilityDistribution
    {
        #region public static (readonly) members

        /// <summary>The Standard Log-Normal distribution, i.e. LogN(0,1).
        /// </summary>
        public static readonly LogNormalDistribution Standard;
        #endregion

        #region private members

        /// <summary>A delegate called if specific data should be stored in <see cref="FillInfoOutput(InfoOutput, string)"/>; perhaps <c>null</c>
        /// </summary>
        private Action<InfoOutput, string> m_InfoOutputAction;

        /// <summary>Represents the value of Exp(\mu).
        /// </summary>
        private readonly double m_ExpOfMu;
        #endregion

        #region public constructors

        /// <summary>Initializes a new instance of the <see cref="LogNormalDistribution"/> class.
        /// </summary>
        /// <param name="mu">The parameter \mu.</param>
        /// <param name="sigma">The parameter \sigma.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, if one of the arguments is not valid.</exception>
        public LogNormalDistribution(double mu, double sigma)
            : this(mu, sigma, infoOutputAction: null)
        {
        }
        #endregion

        #region internal protected constructors

        /// <summary>Initializes a new instance of the <see cref="LogNormalDistribution"/> class.
        /// </summary>
        /// <param name="mu">The parameter \mu.</param>
        /// <param name="sigma">The parameter \sigma.</param>
        /// <param name="infoOutputAction">A delegate called if specific data should be stored in <see cref="FillInfoOutput(InfoOutput, string)"/>; perhaps <c>null</c>.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, if one of the arguments is not valid.</exception>
        internal protected LogNormalDistribution(double mu, double sigma, Action<InfoOutput, string> infoOutputAction)
        {
            m_InfoOutputAction = infoOutputAction;

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException("sigma", String.Format(ExceptionMessages.ArgumentOutOfRange, sigma));
            }
            Sigma = sigma;
            if (Double.IsNaN(mu))
            {
                throw new ArgumentOutOfRangeException("mu", String.Format(ExceptionMessages.ArgumentIsNaN, mu));
            }
            Mu = mu;
            LongName = new IdentifierString(String.Format("LogNormal distribution LN({0}, {1})", Mu, Sigma * Sigma));
            Name = new IdentifierString(String.Format("LN({0}, {1})", Mu, Sigma * Sigma));
            Moment = new MomentCalculator(this);

            Median = m_ExpOfMu = Math.Exp(mu);
        }
        #endregion

        #region static constructor

        /// <summary>Initializes the <see cref="LogNormalDistribution" /> class.
        /// </summary>
        static LogNormalDistribution()
        {
            Standard = new LogNormalDistribution(0.0, 1.0);
        }
        #endregion

        #region public properties

        #region IIdentifierNameable Members

        /// <summary>Gets the name of the current instance.
        /// </summary>
        /// <value>The language independent name of the current instance.</value>
        public IdentifierString Name
        {
            get;
            private set;
        }

        /// <summary>Gets the long name of the current instance.
        /// </summary>
        /// <value>The (perhaps) language dependent long name of the current instance.</value>
        public IdentifierString LongName
        {
            get;
            private set;
        }
        #endregion

        #region IInfoOutputQueriable Members

        /// <summary>Gets the info-output level of detail.
        /// </summary>
        /// <value>The info-output level of detail.</value>
        public InfoOutputDetailLevel InfoOutputDetailLevel
        {
            get { return InfoOutputDetailLevel.Full; }
        }
        #endregion

        #region IProbabilityDistribution Members

        /// <summary>Gets the infimum of the support of the distribution.
        /// </summary>
        /// <value>The infimum of the support of the distribution.</value>
        public double Infimum
        {
            get { return 0.0; }
        }

        /// <summary>Gets the supremum of the support of the distribution.
        /// </summary>
        /// <value>The supremum of the support of the distribution.</value>
        public double Supremum
        {
            get { return Double.PositiveInfinity; }
        }

        /// <summary>Gets the (raw, central etc.) moments of the probability distribution.
        /// </summary>
        /// <value>The (raw, central etc.) moments of the probability distribution.</value>
        public ProbabilityDistributionMoments Moment
        {
            get;
            private set;
        }

        /// <summary>Gets the median.
        /// </summary>
        /// <value>The median.</value>
        public double Median
        {
            get;
            private set;
        }
        #endregion

        /// <summary>Gets the parameter \mu of the log-normal distribution.
        /// </summary>
        /// <value>The parameter \mu of the log-normal distribution.</value>
        public double Mu
        {
            get;
            private set;
        }

        /// <summary>Gets the parameter \sigma of the log-normal distribution.
        /// </summary>
        /// <value>The parameter \sigma of the log-normal distribution.</value>
        public double Sigma
        {
            get;
            private set;
        }
        #endregion

        #region public methods

        #region IProbabilityDistribution Members

        /// <summary>Gets a specific value of the cumulative distribution function.
        /// </summary>
        /// <param name="x">The value where to evaluate.</param>
        /// <returns>The specified value of the cumulative distribution function.</returns>
        public double GetCdfValue(double x)
        {
            if (x <= 0)
            {
                return 0.0;
            }
            return StandardNormalDistribution.GetCdfValue((Math.Log(x) - Mu) / Sigma);
        }

        /// <summary>Gets a specific value of the inverse of the cumulative distribution function.
        /// </summary>
        /// <param name="probability">The probability where to evaluate.</param>
        /// <returns>The specified value of the inverse of the cumulative distribution function.</returns>
        public double GetInverseCdfValue(double probability)
        {
            return Math.Exp(StandardNormalDistribution.GetInverseCdfValue(probability) * Sigma + Mu);
        }

        /// <summary>Gets a specific value of the probability density function.
        /// </summary>
        /// <param name="x">The point where to evaluate.</param>
        /// <returns>The specified value of the probability density function.</returns>
        public double GetPdfValue(double x)
        {
            if (x <= 0)
            {
                return 0.0;
            }
            double logOfX = Math.Log(x);
            return 1.0 / (Sigma * x * MathConsts.SqrtTwoPi) * Math.Exp(-(logOfX - Mu) * (logOfX - Mu) / (2 * Sigma * Sigma));
        }

        /// <summary>Gets a specific value of the characteristic function E[exp(itX)].
        /// </summary>
        /// <param name="t">The point where to evaluate the characteristic function.</param>
        /// <returns>The specified value of the characteristic function E[exp(itX)].</returns>
        /// <remarks>The implementation is based on "On the Laplace transformation of the Lognormal distribution", S. Asmussen, J. L. Jensen, L. Rojas-Nandayapa.</remarks>
        public Complex GetChfValue(double t)
        {
            var w = SpecialFunction.IteratedExponential.LambertW(-t * Sigma * Sigma * m_ExpOfMu * Complex.ImaginaryOne);
            return Complex.Exp(-(w * w + 2 * w) / (2 * Sigma * Sigma)) / Complex.Sqrt(1 + w);
        }
        #endregion

        #region IInfoOutputQueriable Members

        /// <summary>Sets the <see cref="IInfoOutputQueriable.InfoOutputDetailLevel" /> property.
        /// </summary>
        /// <param name="infoOutputDetailLevel">The info-output level of detail.</param>
        /// <returns>A value indicating whether the <see cref="IInfoOutputQueriable.InfoOutputDetailLevel" /> has been set to <paramref name="infoOutputDetailLevel" />.</returns>
        public bool TrySetInfoOutputDetailLevel(InfoOutputDetailLevel infoOutputDetailLevel)
        {
            return (infoOutputDetailLevel == InfoOutputDetailLevel.Full);
        }

        /// <summary>Gets informations of the current object as a specific <see cref="InfoOutput" /> instance.
        /// </summary>
        /// <param name="infoOutput">The <see cref="InfoOutput" /> object which is to be filled with informations concering the current instance.</param>
        /// <param name="categoryName">The name of the category, i.e. all informations will be added to these category.</param>
        public void FillInfoOutput(InfoOutput infoOutput, string categoryName = InfoOutput.GeneralCategoryName)
        {
            var infoOutputPackage = infoOutput.AcquirePackage(categoryName);

            infoOutputPackage.Add("Distribution", "LogNormal");
            infoOutputPackage.Add("Mu", Mu);
            infoOutputPackage.Add("Sigma", Sigma);
            if (m_InfoOutputAction != null)
            {
                m_InfoOutputAction(infoOutput, categoryName);
            }
        }
        #endregion

        /// <summary>Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return String.Format("Log({0},{1})", Mu, Sigma);
        }
        #endregion

        #region public static methods

        /// <summary>Creates a new <see cref="LogNormalDistribution"/> instance.
        /// </summary>
        /// <param name="mu">The parameter \mu.</param>
        /// <param name="sigma">The parameter \sigma.</param>
        /// <returns>The <see cref="LogNormalDistribution"/> object with respect to the specified mean and standard deviation.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, if one of the arguments is not valid.</exception>
        public static LogNormalDistribution Create(double mu, double sigma)
        {
            return new LogNormalDistribution(mu, sigma);
        }
        #endregion

        #region IProbabilityDistribution Members


        public void GetCdfValue(int n, double[] a, double[] y)
        {
            throw new NotImplementedException();
        }

        public void GetInverseCdfValue(int n, double[] a, double[] y)
        {
            throw new NotImplementedException();
        }

        public void GetPdfValue(int n, double[] a, double[] y)
        {
            throw new NotImplementedException();
        }

        public void GetChfValue(int n, double[] a, Complex[] y)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}