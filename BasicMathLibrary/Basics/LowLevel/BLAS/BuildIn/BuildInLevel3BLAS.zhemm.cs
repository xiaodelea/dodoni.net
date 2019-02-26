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

namespace Dodoni.MathLibrary.Basics.LowLevel.BuildIn
{
    /// <summary>Serves as (primitive) managed code implementation of BLAS level 3 operations (fall-back solution).
    /// </summary>
    /// <remarks>This implementation is based on the C code of the BLAS implementation, see http://www.netlib.org/clapack/cblas. </remarks>
    internal partial class BuildInLevel3BLAS
    {
        /// <summary>Computes a matrix-matrix product where one input matrix is Hermitian, i.e. C := \alpha*A*B + \beta*C or C := \alpha*B*A + \beta*C, where A is a Hermitian matrix.
        /// </summary>
        /// <param name="m">The number of rows of matrix C.</param>
        /// <param name="n">The number of columns of matrix C.</param>
        /// <param name="alpha">The scalar \alpha.</param>
        /// <param name="a">The Hermitian matrix A supplied column-by-column of dimension (<paramref name="ldc" />, ka), where ka is <paramref name="m" /> if to calculate C := \alpha*A*B + \beta*C; <paramref name="n" /> otherwise.</param>
        /// <param name="b">The matrix B supplied column-by-column of dimension (<paramref name="ldb" />, <paramref name="n" />).</param>
        /// <param name="beta">The scalar \beta.</param>
        /// <param name="c">The matrix C supplied column-by-column of dimension (<paramref name="ldc" />, <paramref name="n" />).</param>
        /// <param name="lda">The leading dimension of <paramref name="a" />, must be at least max(1,<paramref name="m" />) if to calculate C := \alpha*A*B + \beta*C; max(1, <paramref name="n" />) otherwise.</param>
        /// <param name="ldb">The leading dimension of <paramref name="b" />, must be at least max(1,<paramref name="m" />).</param>
        /// <param name="ldc">The leading dimension of <paramref name="c" />, must be at least max(1, <paramref name="m" />).</param>
        /// <param name="side">A value indicating whether to calculate C := \alpha*A*B + \beta*C or C := \alpha*B*A + \beta*C.</param>
        /// <param name="triangularMatrixType">A value whether matrix A is in its upper or lower triangular representation.</param>
        public void zhemm(int m, int n, Complex alpha, ReadOnlySpan<Complex> a, ReadOnlySpan<Complex> b, Complex beta, Span<Complex> c, int lda, int ldb, int ldc, BLAS.Side side = BLAS.Side.Left, BLAS.TriangularMatrixType triangularMatrixType = BLAS.TriangularMatrixType.UpperTriangularMatrix)
        {
            if (m == 0 || n == 0 || ((alpha == 0.0) && (beta == 1.0)))
            {
                return; // nothing to do
            }

            if (side == BLAS.Side.Left)  // C = \alpha *A *B +\beta*C
            {
                if (triangularMatrixType == BLAS.TriangularMatrixType.UpperTriangularMatrix)
                {
                    for (int j = 0; j < n; j++)
                    {
                        for (int i = 0; i < m; i++)
                        {
                            var temp = alpha * b[i + j * lda];

                            Complex temp2 = 0.0;
                            for (int k = 0; k <= i - 1; k++)
                            {
                                c[k + j * ldc] += temp * a[k + i * lda];
                                temp2 += b[k + j * ldb] * Complex.Conjugate(a[k + i * lda]);
                            }
                            c[i + j * ldc] = beta * c[i + j * ldc] + a[i + i * lda].Real * temp.Real + Complex.ImaginaryOne * a[i + i * lda].Real * temp.Imaginary + alpha * temp2;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        for (int i = m - 1; i >= 0; i--)
                        {
                            Complex temp = alpha * b[i + j * ldb];
                            Complex temp2 = 0.0;

                            for (int k = i + 1; k < m; k++)
                            {
                                c[k + j * ldc] += temp * a[k + i * lda];
                                temp2 += b[k + j * ldb] * Complex.Conjugate(a[k + i * lda]);
                            }
                            c[i + j * ldc] = beta * c[i + j * ldc] + alpha * temp2 + a[i + i * lda].Real * temp.Real + Complex.ImaginaryOne * (a[i + i * lda].Real * temp.Imaginary);
                        }
                    }
                }
            }
            else if (side == BLAS.Side.Right)  // C = \alpha*B*A + \beta *C
            {
                for (int j = 0; j < n; j++)
                {
                    Complex temp = alpha * a[j + j * lda].Real;
                    for (int i = 0; i < m; i++)
                    {
                        c[i + j * ldc] = beta * c[i + j * ldc] + temp * b[i + j * ldb];
                    }

                    for (int k = 0; k <= j - 1; k++)
                    {
                        if (triangularMatrixType == BLAS.TriangularMatrixType.UpperTriangularMatrix)
                        {
                            temp = alpha * a[k + j * lda];
                        }
                        else
                        {
                            temp = alpha * Complex.Conjugate(a[j + k * lda]);
                        }
                        for (int i = 0; i < m; i++)
                        {
                            c[i + j * ldc] += temp * b[i + k * ldb];
                        }
                    }
                    for (int k = j + 1; k < n; k++)
                    {
                        if (triangularMatrixType == BLAS.TriangularMatrixType.UpperTriangularMatrix)
                        {
                            temp = alpha * Complex.Conjugate(a[j + k * lda]);
                        }
                        else
                        {
                            temp = alpha * a[k + j * lda];
                        }
                        for (int i = 0; i < m; i++)
                        {
                            c[i + j * ldc] += temp * b[i + k * ldb];
                        }
                    }
                }
            }
            else
            {
                throw new NotImplementedException(side.ToString());
            }
        }
    }
}