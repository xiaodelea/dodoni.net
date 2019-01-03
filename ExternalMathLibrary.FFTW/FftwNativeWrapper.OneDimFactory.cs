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

namespace Dodoni.MathLibrary.Basics.LowLevel.Native
{
    public partial class FftwNativeWrapper
    {
        /// <summary>Represents a factory for one-dimensional Fast-Fourier transformations with respect to the FFTW library, release 3.3.x.
        /// </summary>
        /// <remarks>Non-free lizenses of the FFTW library are available, for more informations, see http://www.fftw.org
        /// Copyright (c) 2003, 2007-8 Matteo Frigo 
        /// Copyright (c) 2003, 2007-8 Massachusetts Institute of Technology
        /// </remarks>
        protected class FftwOneDimFourierTransformationFactory : IOneDimFourierTransformationFactory
        {
            #region public constructors

            /// <summary>Initializes a new instance of the <see cref="FftwOneDimFourierTransformationFactory"/> class.
            /// </summary>
            public FftwOneDimFourierTransformationFactory()
            {
            }
            #endregion

            #region public methods

            #region FFT.IOneDimFourierTransformationFactory Members

            /// <summary>Gets a specific one-dimensional Fourier transformation.
            /// </summary>
            /// <param name="length">The length, i.e. the number of complex coefficients.</param>
            /// <returns>A specific one-dimensional Fourier transformation.
            /// </returns>
            public FFT.IOneDimensional Create(int length)
            {
                return new FftwOneDimFourierTransformation(length);
            }

            /// <summary>Gets a specific one-dimensional real Fourier transformation.
            /// </summary>
            /// <param name="length">The length, i.e. the number of real (input) coefficients.</param>
            /// <returns>A specific one-dimensional real Fourier transformation.
            /// </returns>
            public FFT.IOneDimensionalRealData CreateRealTransformation(int length)
            {
                return new FftwOneDimRealDataFourierTransform(length);
            }
            #endregion

            /// <summary>Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>A <see cref="System.String"/> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return "FFTW 3.3.x";
            }
            #endregion
        }
    }
}