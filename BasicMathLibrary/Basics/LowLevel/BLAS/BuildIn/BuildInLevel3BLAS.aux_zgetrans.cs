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
        /// <summary>Gets a optimal workspace array length for the <c>aux_zgetrans</c> function.
        /// </summary>
        /// <param name="rowCount">The number of rows.</param>
        /// <param name="columnCount">The number of columns.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int aux_zgetransQuery(int rowCount, int columnCount)
        {
            return Math.Max(rowCount, columnCount);
        }

        /// <summary>Performs in-place transposition of a specific matrix.
        /// </summary>
        /// <param name="rowCount">The number of rows.</param>
        /// <param name="columnCount">The number of columns.</param>
        /// <param name="a">The matrix provided column-by-column (column-major ordering).</param>
        /// <param name="work">A workspace array.</param>
        /// <remarks>The implementation is base on 'A decomposition for In-place Matrix Transposition', Bryan Catanzaro, Alexander Keller, Michael Garland; 2014.</remarks>
        public void aux_zgetrans(int rowCount, int columnCount, Complex[] a, Complex[] work = null)
        {
            int lwork = Math.Max(rowCount, columnCount);
            if ((work == null) || (work.Length < lwork))
            {
                work = new Complex[lwork];
            }
            int m = columnCount;
            int n = rowCount;

            int c = gcd(m, n);
            int b = n / c;
            int e = m / c;  // = a in the original paper

            if (c > 1)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int i = 0; i < m; i++)
                    {
                        int r = (i + j / b) % m;
                        work[i] = a[r + columnCount * j];
                    }
                    for (int i = 0; i < m; i++)
                    {
                        a[i + columnCount * j] = work[i];
                    }
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int d = ((i + j / b) % m + j * m) % n;
                    work[d] = a[i + columnCount * j];
                }
                for (int j = 0; j < n; j++)
                {
                    a[i + columnCount * j] = work[j];
                }
            }

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < m; i++)
                {
                    int s = (j + i * n - (i / e)) % m;
                    work[i] = a[s + columnCount * j];
                }
                for (int i = 0; i < m; i++)
                {
                    a[i + columnCount * j] = work[i];
                }
            }
        }
    }
}