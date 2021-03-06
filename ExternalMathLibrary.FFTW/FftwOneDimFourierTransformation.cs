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

namespace Dodoni.MathLibrary.Basics.LowLevel.Native
{
    /// <summary>Represents the wrapper for FFTW's one-dimensional discrete Fourier Transformation implementation.
    /// </summary>
    internal class FftwOneDimFourierTransformation : FftwOneDimDiscreteFourierTransformation, FFT.IOneDimensional
    {
        #region internal constructors

        /// <summary>Initializes a new instance of the <see cref="FftwOneDimFourierTransformation"/> class.
        /// </summary>
        /// <param name="length">The length, i.e. the number of Fourier coefficients.</param>
        internal FftwOneDimFourierTransformation(int length)
            : base(length)
        {
        }
        #endregion

        #region public properties

        #region FFT.IOneDimensional Members

        /// <summary>Gets the scaling factor \alpha, i.e. 1/<see cref="FFT.IOneDimensional.Length"/> in the case of the FFT and an arbritrary value in the case of a Fractional FFT.
        /// </summary>
        /// <value>The scaling factor \alpha.</value>
        public Complex Alpha
        {
            get { return 1.0 / Length; }
        }

        /// <summary>Gets a value indicating the restriction of the parameter \alpha in the Fourier transformation.
        /// </summary>
        /// <value>The restriction of the parameter \alpha in the Fourier transformation.</value>
        public FFT.FourierExponentialFactorRestriction FourierExponentialFactorRestriction
        {
            get { return FFT.FourierExponentialFactorRestriction.OneOverLength; }
        }
        #endregion

        #endregion

        #region public methods

        #region FFT.IOneDimensional Members

        /// <summary>Sets the scaling factor \alpha in the Fourier Transformation.
        /// </summary>
        /// <param name="alpha">The scaling factor \alpha.</param>
        /// <exception cref="InvalidOperationException">Thrown if <see cref="FFT.IOneDimensional.FourierExponentialFactorRestriction"/> indicates that the parameter \alpha can not be changed in this way or <paramref name="alpha"/> contains an invalid value.</exception>
        public void SetParameterAlpha(Complex alpha)
        {
            throw new InvalidOperationException();
        }

        /// <summary>Sets the scaling factor \alpha in the Fourier Transformation.
        /// </summary>
        /// <param name="alpha">The scaling factor \alpha.</param>
        /// <exception cref="InvalidOperationException">Thrown if <see cref="FFT.IOneDimensional.FourierExponentialFactorRestriction"/> indicates that the parameter \alpha can not be changed in this way or <paramref name="alpha"/> contains an invalid value.</exception>
        public void SetParameterAlpha(double alpha)
        {
            throw new InvalidOperationException();
        }

        /// <summary>Compute the forward Fourier transformation.
        /// </summary>
        /// <param name="fourierCoefficients">The input as well as the output [in place] with at least <see cref="FFT.IOneDimensional.Length"/> elements.</param>
        /// <param name="scalingFactor">The scaling factor with respect to the input domain.</param>
        public void ForwardTransformation(Complex[] fourierCoefficients, double scalingFactor)
        {
            CreateOneDimPlan(fourierCoefficients, fourierCoefficients, FFTW_FORWARD);
            ExecudePlan();
            BLAS.Level1.zscal(Length, scalingFactor, fourierCoefficients);
        }

        /// <summary>Compute the forward Fourier transformation.
        /// </summary>
        /// <param name="fourierCoefficients">The input as well as the output [in place] with at least <see cref="FFT.IOneDimensional.Length"/> elements.</param>
        /// <remarks>The scaling factor is assumed to be <c>1.0</c>.</remarks>
        public void ForwardTransformation(Complex[] fourierCoefficients)
        {
            CreateOneDimPlan(fourierCoefficients, fourierCoefficients, FFTW_FORWARD);
            ExecudePlan();
        }

        /// <summary>Compute the forward Fourier transformation.
        /// </summary>
        /// <param name="inputFourierCoefficients">The input Fourier coefficients with at least <see cref="FFT.IOneDimensional.Length"/> elements.</param>
        /// <param name="outputFourierCoefficients">The output Fourier coefficients, i.e. out-of-place calculation with at
        /// least <see cref="FFT.IOneDimensional.Length"/> elements (output).</param>
        /// <param name="scalingFactor">The scaling factor with respect to the input domain.</param>
        public void ForwardTransformation(Complex[] inputFourierCoefficients, Complex[] outputFourierCoefficients, double scalingFactor)
        {
            CreateOneDimPlan(inputFourierCoefficients, outputFourierCoefficients, FFTW_FORWARD);
            ExecudePlan();
            BLAS.Level1.zscal(Length, scalingFactor, outputFourierCoefficients);
        }

        /// <summary>Compute the forward Fourier transformation.
        /// </summary>
        /// <param name="inputFourierCoefficients">The input Fourier coefficients with at least <see cref="FFT.IOneDimensional.Length"/> elements.</param>
        /// <param name="outputFourierCoefficients">The output Fourier coefficients, i.e. out-of-place calculation with at
        /// least <see cref="FFT.IOneDimensional.Length"/> elements (output).</param>
        /// <remarks>The scaling factor is assumed to be <c>1.0</c>.</remarks>
        public void ForwardTransformation(Complex[] inputFourierCoefficients, Complex[] outputFourierCoefficients)
        {
            CreateOneDimPlan(inputFourierCoefficients, outputFourierCoefficients, FFTW_FORWARD);
            ExecudePlan();
        }

        /// <summary>Compute the backward Fourier transformation.
        /// </summary>
        /// <param name="fourierCoefficients">The input as well as the output [in place] with at least <see cref="FFT.IOneDimensional.Length"/> elements.</param>
        /// <param name="scalingFactor">The scaling factor with respect to the input domain.</param>
        public void BackwardTransformation(Complex[] fourierCoefficients, double scalingFactor)
        {
            CreateOneDimPlan(fourierCoefficients, fourierCoefficients, FFTW_BACKWARD);
            ExecudePlan();
            BLAS.Level1.zscal(Length, scalingFactor, fourierCoefficients);
        }

        /// <summary>Compute the backward Fourier transformation.
        /// </summary>
        /// <param name="fourierCoefficients">The input as well as the output [in place] with at least <see cref="FFT.IOneDimensional.Length"/> elements.</param>
        /// <remarks>The scaling factor is assumed to be <c>1.0</c>.</remarks>
        public void BackwardTransformation(Complex[] fourierCoefficients)
        {
            CreateOneDimPlan(fourierCoefficients, fourierCoefficients, FFTW_BACKWARD);
            ExecudePlan();
        }

        /// <summary>Compute the backward Fourier transformation.
        /// </summary>
        /// <param name="inputFourierCoefficients">The input Fourier coefficients with at least <see cref="FFT.IOneDimensional.Length"/> elements.</param>
        /// <param name="outputFourierCoefficients">The output Fourier coefficients, i.e. out-of-place calculation with at
        /// least <see cref="FFT.IOneDimensional.Length"/> elements (output).</param>
        /// <param name="scalingFactor">The scaling factor with respect to the input domain.</param>
        public void BackwardTransformation(Complex[] inputFourierCoefficients, Complex[] outputFourierCoefficients, double scalingFactor)
        {
            CreateOneDimPlan(inputFourierCoefficients, outputFourierCoefficients, FFTW_BACKWARD);
            ExecudePlan();
            BLAS.Level1.zscal(Length, scalingFactor, outputFourierCoefficients);
        }

        /// <summary>Compute the backward Fourier transformation.
        /// </summary>
        /// <param name="inputFourierCoefficients">The input Fourier coefficients with at least <see cref="FFT.IOneDimensional.Length"/> elements.</param>
        /// <param name="outputFourierCoefficients">The output Fourier coefficients, i.e. out-of-place calculation with at
        /// least <see cref="FFT.IOneDimensional.Length"/> elements (output).</param>
        /// <remarks>The scaling factor is assumed to be <c>1.0</c>.</remarks>
        public void BackwardTransformation(Complex[] inputFourierCoefficients, Complex[] outputFourierCoefficients)
        {
            CreateOneDimPlan(inputFourierCoefficients, outputFourierCoefficients, FFTW_BACKWARD);
            ExecudePlan();
        }
        #endregion

        #endregion
    }
}