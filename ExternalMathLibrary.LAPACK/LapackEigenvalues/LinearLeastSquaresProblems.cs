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
using System.Security;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Dodoni.MathLibrary.Basics.LowLevel.Native
{
    /// <summary>The native wrapper for <see cref="LapackEigenvalues.ILinearLeastSquaresProblems"/>.
    /// </summary>   
    internal class LinearLeastSquaresProblems : LapackEigenvalues.ILinearLeastSquaresProblems
    {
        #region private function import

#if LOWER_CASE_UNDERSCORE

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dgels_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dgels(ref char trans, ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dgels_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dgels(ref char trans, ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, double* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zgels_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zgels(ref char trans, ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] Complex[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zgels_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zgels(ref char trans, ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, Complex* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dgelsy_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dgelsy(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] int[] jpvt, ref double rcond, out int rank, [In, Out] double[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dgelsy_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dgelsy(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] int[] jpvt, ref double rcond, out int rank, double* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zgelsy_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zgelsy(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] int[] jpvt, ref double rcond, out int rank, [In, Out] Complex[] work, ref int lwork, [In, Out] double[] rwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zgelsy_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zgelsy(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] int[] jpvt, ref double rcond, out int rank, Complex* work, ref int lwork, [In, Out] double[] rwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dgelss_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dgelss(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, [In, Out] double[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dgelss_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dgelss(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, double* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zgelss_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zgelss(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, [In, Out] Complex[] work, ref int lwork, [In, Out] double[] rwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zgelss_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zgelss(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, Complex* work, ref int lwork, [In, Out] double[] rwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dgelsd_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dgelsd(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, [In, Out] double[] work, ref int lwork, [In, Out] int[] iwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dgelsd_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dgelsd(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, double* work, ref int lwork, int* iwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zgelsd_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zgelsd(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, [In, Out] Complex[] work, ref int lwork, [In, Out] double[] rwork, [In, Out] int[] iwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zgelsd_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zgelsd(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, Complex* work, ref int lwork, double* rwork, int* iwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dgglse_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dgglse(ref int m, ref int n, ref int p, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] c, [In, Out] double[] d, [In, Out] double[] x, [In, Out] double[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dgglse_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dgglse(ref int m, ref int n, ref int p, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] c, [In, Out] double[] d, [In, Out] double[] x, double* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zgglse_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zgglse(ref int m, ref int n, ref int p, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] Complex[] c, [In, Out] Complex[] d, [In, Out] Complex[] x, [In, Out] Complex[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zgglse_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zgglse(ref int m, ref int n, ref int p, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] Complex[] c, [In, Out] Complex[] d, [In, Out] Complex[] x, Complex* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dggglm_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dggglm(ref int n, ref int m, ref int p, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] d, [In, Out] double[] x, [In, Out] double[] y, [In, Out] double[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "dggglm_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dggglm(ref int n, ref int m, ref int p, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] d, [In, Out] double[] x, [In, Out] double[] y, double* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zggglm_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zggglm(ref int n, ref int m, ref int p, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] Complex[] d, [In, Out] Complex[] x, [In, Out] Complex[] y, [In, Out] Complex[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.dllName, EntryPoint = "zggglm_", CallingConvention = LapackNativeWrapper.CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zggglm(ref int n, ref int m, ref int p, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] Complex[] d, [In, Out] Complex[] x, [In, Out] Complex[] y, Complex* work, ref int lwork, out int info);
#else
        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGELS", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dgels(ref char trans, ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGELS", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dgels(ref char trans, ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, double* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGELS", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zgels(ref char trans, ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] Complex[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGELS", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zgels(ref char trans, ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, Complex* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGELSY", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dgelsy(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] int[] jpvt, ref double rcond, out int rank, [In, Out] double[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGELSY", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dgelsy(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] int[] jpvt, ref double rcond, out int rank, double* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGELSY", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zgelsy(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] int[] jpvt, ref double rcond, out int rank, [In, Out] Complex[] work, ref int lwork, [In, Out] double[] rwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGELSY", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zgelsy(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] int[] jpvt, ref double rcond, out int rank, Complex* work, ref int lwork, [In, Out] double[] rwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGELSS", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dgelss(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, [In, Out] double[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGELSS", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dgelss(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, double* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGELSS", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zgelss(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, [In, Out] Complex[] work, ref int lwork, [In, Out] double[] rwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGELSS", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zgelss(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, Complex* work, ref int lwork, [In, Out] double[] rwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGELSD", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dgelsd(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, [In, Out] double[] work, ref int lwork, [In, Out] int[] iwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGELSD", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dgelsd(ref int m, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, double* work, ref int lwork, int* iwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGELSD", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zgelsd(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, [In, Out] Complex[] work, ref int lwork, [In, Out] double[] rwork, [In, Out] int[] iwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGELSD", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zgelsd(ref int m, ref int n, ref int nrhs, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] double[] s, ref double rcond, out int rank, Complex* work, ref int lwork, double* rwork, int* iwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGGLSE", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dgglse(ref int m, ref int n, ref int p, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] c, [In, Out] double[] d, [In, Out] double[] x, [In, Out] double[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGGLSE", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dgglse(ref int m, ref int n, ref int p, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] c, [In, Out] double[] d, [In, Out] double[] x, double* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGGLSE", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zgglse(ref int m, ref int n, ref int p, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] Complex[] c, [In, Out] Complex[] d, [In, Out] Complex[] x, [In, Out] Complex[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGGLSE", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zgglse(ref int m, ref int n, ref int p, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] Complex[] c, [In, Out] Complex[] d, [In, Out] Complex[] x, Complex* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGGGLM", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_dggglm(ref int n, ref int m, ref int p, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] d, [In, Out] double[] x, [In, Out] double[] y, [In, Out] double[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "DGGGLM", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_dggglm(ref int n, ref int m, ref int p, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, [In, Out] double[] d, [In, Out] double[] x, [In, Out] double[] y, double* work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGGGLM", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern void _driver_zggglm(ref int n, ref int m, ref int p, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] Complex[] d, [In, Out] Complex[] x, [In, Out] Complex[] y, [In, Out] Complex[] work, ref int lwork, out int info);

        [DllImport(LapackNativeWrapper.sm_DllName, EntryPoint = "ZGGGLM", CallingConvention = LapackNativeWrapper.sm_CallingConvention), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void _driver_zggglm(ref int n, ref int m, ref int p, [In, Out] Complex[] a, ref int lda, [In, Out] Complex[] b, ref int ldb, [In, Out] Complex[] d, [In, Out] Complex[] x, [In, Out] Complex[] y, Complex* work, ref int lwork, out int info);
#endif
        #endregion

        #region public methods

        /// <summary>Gets a optimal workspace array length for the <c>dgels</c> function.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="transposeState">A value indicating whether to take into account A or A'.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_dgelsQuery(int m, int n, int nrhs, BLAS.MatrixTransposeState transposeState = BLAS.MatrixTransposeState.NoTranspose)
        {
            int info;
            var trans = LapackNativeWrapper.GetTrans(transposeState);

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = -1;

            unsafe
            {
                double* work = stackalloc double[1];

                _driver_dgels(ref trans, ref m, ref n, ref nrhs, null, ref lda, null, ref ldb, work, ref lwork, out info);
                LapackNativeWrapper.CheckForError(info, "dgels");

                return ((int)work[0]) + 1;
            }
        }

        /// <summary>Uses QR or LQ factorization to solve a overdetermined or underdetermined linear system with full rank matrix, i.e. minimize ||b - op(A) * x||.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="a">The <paramref name="m"/>-by-<paramref name="n"/> matrix provided column-by-column.</param>
        /// <param name="b">The <paramref name="m"/>-by-<paramref name="nrhs"/> matrix B of right hand side vectors, stored columnwise.</param>
        /// <param name="work">A workspace array.</param>
        /// <param name="transposeState">A value indicating whether to take into account A or A'.</param>
        public void driver_dgels(int m, int n, int nrhs, double[] a, double[] b, double[] work, BLAS.MatrixTransposeState transposeState = BLAS.MatrixTransposeState.NoTranspose)
        {
            int info;
            var trans = LapackNativeWrapper.GetTrans(transposeState);

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = work.Length;

            _driver_dgels(ref trans, ref m, ref n, ref nrhs, a, ref lda, b, ref ldb, work, ref lwork, out info);
            LapackNativeWrapper.CheckForError(info, "dgels");
        }

        /// <summary>Gets a optimal workspace array length for the <c>zgels</c> function.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="transposeState">A value indicating whether to take into account A or A'.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_zgelsQuery(int m, int n, int nrhs, BLAS.MatrixTransposeState transposeState = BLAS.MatrixTransposeState.NoTranspose)
        {
            int info;
            var trans = LapackNativeWrapper.GetTrans(transposeState);

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = -1;

            unsafe
            {
                Complex* work = stackalloc Complex[1];

                _driver_zgels(ref trans, ref m, ref n, ref nrhs, null, ref lda, null, ref ldb, work, ref lwork, out info);
                LapackNativeWrapper.CheckForError(info, "zgels");

                return ((int)work[0].Real) + 1;
            }
        }

        /// <summary>Uses QR or LQ factorization to solve a overdetermined or underdetermined linear system with full rank matrix, i.e. minimize ||b - op(A) * x||.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="a">The <paramref name="m"/>-by-<paramref name="n"/> matrix provided column-by-column.</param>
        /// <param name="b">The <paramref name="m"/>-by-<paramref name="nrhs"/> matrix B of right hand side vectors, stored columnwise.</param>
        /// <param name="work">A workspace array.</param>
        /// <param name="transposeState">A value indicating whether to take into account A or A'.</param>
        public void driver_zgels(int m, int n, int nrhs, Complex[] a, Complex[] b, Complex[] work, BLAS.MatrixTransposeState transposeState = BLAS.MatrixTransposeState.NoTranspose)
        {
            int info;
            var trans = LapackNativeWrapper.GetTrans(transposeState);

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = work.Length;

            _driver_zgels(ref trans, ref m, ref n, ref nrhs, a, ref lda, b, ref ldb, work, ref lwork, out info);
            LapackNativeWrapper.CheckForError(info, "zgels");
        }

        /// <summary>Gets a optimal workspace array length for the <c>dgelsy</c> function.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="rcond">Is used to determine the effective rank of A, which is defined as the order of the largest leading triangular submatrix R_11 in the 
        /// QR factorization with pivoting of A, whose estimated condition number &lt; 1 / rcond.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_dgelsyQuery(int m, int n, int nrhs, double rcond)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = -1;
            int rank;

            unsafe
            {
                double* work = stackalloc double[1];

                _driver_dgelsy(ref m, ref n, ref nrhs, null, ref lda, null, ref ldb, null, ref rcond, out rank, work, ref lwork, out info);
                LapackNativeWrapper.CheckForError(info, "dgelsy");

                return ((int)work[0]) + 1;
            }
        }

        /// <summary>Computes the minimum-norm solution to a linear least squares problem using a complete orthogonal factorization of A, i.e. minimize ||b - A * x||.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="a">The <paramref name="m"/>-by-<paramref name="n"/> matrix provided column-by-column.</param>
        /// <param name="b">The <paramref name="m"/>-by-<paramref name="nrhs"/> matrix B of right hand side vectors, stored columnwise.</param>
        /// <param name="jpvt">On entry, if jpvt[j] != 0, the i-th column of A is permuted to the front of AP, otherwise i-th column of A is a free column.</param>
        /// <param name="rcond">Is used to determine the effective rank of A, which is defined as the order of the largest leading triangular submatrix R_11 in the 
        /// QR factorization with pivoting of A, whose estimated condition number &lt; 1 / rcond.</param>
        /// <param name="work">A workspace array.</param>
        /// <param name="rank">The effective rank of A, which is defined as the order of the largest leading triangular submatrix R_11 in the 
        /// QR factorization with pivoting of A, whose estimated condition number &lt; 1 / rcond.</param>
        public void driver_dgelsy(int m, int n, int nrhs, double[] a, double[] b, int[] jpvt, double rcond, double[] work, out int rank)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = work.Length;

            _driver_dgelsy(ref m, ref n, ref nrhs, a, ref lda, b, ref ldb, jpvt, ref rcond, out rank, work, ref lwork, out info);
            LapackNativeWrapper.CheckForError(info, "dgelsy");
        }

        /// <summary>Gets a optimal workspace array length for the <c>zgelsy</c> function.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="rcond">Is used to determine the effective rank of A, which is defined as the order of the largest leading triangular submatrix R_11 in the 
        /// QR factorization with pivoting of A, whose estimated condition number &lt; 1 / rcond.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_zgelsyQuery(int m, int n, int nrhs, double rcond)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = -1;
            int rank;

            unsafe
            {
                Complex* work = stackalloc Complex[1];

                _driver_zgelsy(ref m, ref n, ref nrhs, null, ref lda, null, ref ldb, null, ref rcond, out rank, work, ref lwork, null, out info);
                LapackNativeWrapper.CheckForError(info, "zgelsy");

                return ((int)work[0].Real) + 1;
            }
        }

        /// <summary>Computes the minimum-norm solution to a linear least squares problem using a complete orthogonal factorization of A, i.e. minimize ||b - A * x||.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="a">The <paramref name="m"/>-by-<paramref name="n"/> matrix provided column-by-column.</param>
        /// <param name="b">The <paramref name="m"/>-by-<paramref name="nrhs"/> matrix B of right hand side vectors, stored columnwise.</param>
        /// <param name="jpvt">On entry, if jpvt[j] != 0, the i-th column of A is permuted to the front of AP, otherwise i-th column of A is a free column.</param>
        /// <param name="rcond">Is used to determine the effective rank of A, which is defined as the order of the largest leading triangular submatrix R_11 in the 
        /// QR factorization with pivoting of A, whose estimated condition number &lt; 1 / rcond.</param>
        /// <param name="work">A workspace array.</param>
        /// <param name="rwork">A workspace array with dimension at least 2 * <paramref name="n"/>.</param>
        /// <param name="rank">The effective rank of A, which is defined as the order of the largest leading triangular submatrix R_11 in the 
        /// QR factorization with pivoting of A, whose estimated condition number &lt; 1 / rcond.</param>
        public void driver_zgelsy(int m, int n, int nrhs, Complex[] a, Complex[] b, int[] jpvt, double rcond, Complex[] work, double[] rwork, out int rank)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = work.Length;

            _driver_zgelsy(ref m, ref n, ref nrhs, a, ref lda, b, ref ldb, jpvt, ref rcond, out rank, work, ref lwork, rwork, out info);
            LapackNativeWrapper.CheckForError(info, "zgelsy");
        }

        /// <summary>Gets a optimal workspace array length for the <c>dgelss</c> function.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="rcond">Is used to determine the effective rank of A. Singular values s_i &lt;= rcond * s_1 are treated as zero.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_dgelssQuery(int m, int n, int nrhs, double rcond)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = -1;
            int rank;

            unsafe
            {
                double* work = stackalloc double[1];

                _driver_dgelss(ref m, ref n, ref nrhs, null, ref lda, null, ref ldb, null, ref rcond, out rank, work, ref lwork, out info);
                LapackNativeWrapper.CheckForError(info, "dgelss");

                return ((int)work[0]) + 1;
            }
        }

        /// <summary>Computes the minimum-norm solution to a linear least squares problem using the singular value decomposition of A,  i.e. minimize ||b - A * x||.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="a">The <paramref name="m"/>-by-<paramref name="n"/> matrix provided column-by-column.</param>
        /// <param name="b">The <paramref name="m"/>-by-<paramref name="nrhs"/> matrix B of right hand side vectors, stored columnwise.</param>
        /// <param name="s">Contains the singular values of matrix A in decreasing order on exit; dimension at least min(<paramref name="m"/>, <paramref name="n"/>).</param>
        /// <param name="rcond">Is used to determine the effective rank of A. Singular values s_i &lt;= rcond * s_1 are treated as zero.</param>
        /// <param name="work">A workspace array.</param>
        /// <param name="rank">The effective rank of matrix A, that is, the number of singular values which are reater than <paramref name="rcond"/> * s_1 (output).</param>
        public void driver_dgelss(int m, int n, int nrhs, double[] a, double[] b, double[] s, double rcond, double[] work, out int rank)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = work.Length;

            _driver_dgelss(ref m, ref n, ref nrhs, a, ref lda, b, ref ldb, s, ref rcond, out rank, work, ref lwork, out info);
            LapackNativeWrapper.CheckForError(info, "dgelss");

        }

        /// <summary>Gets a optimal workspace array length for the <c>zgelss</c> function.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="rcond">Is used to determine the effective rank of A. Singular values s_i &lt;= rcond * s_1 are treated as zero.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_zgelssQuery(int m, int n, int nrhs, double rcond)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = -1;
            int rank;

            unsafe
            {
                Complex* work = stackalloc Complex[1];

                _driver_zgelss(ref m, ref n, ref nrhs, null, ref lda, null, ref ldb, null, ref rcond, out rank, work, ref lwork, null, out info);
                LapackNativeWrapper.CheckForError(info, "zgelss");

                return ((int)work[0].Real) + 1;
            }
        }

        /// <summary>Computes the minimum-norm solution to a linear least squares problem using the singular value decomposition of A,  i.e. minimize ||b - A * x||.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="a">The <paramref name="m"/>-by-<paramref name="n"/> matrix provided column-by-column.</param>
        /// <param name="b">The <paramref name="m"/>-by-<paramref name="nrhs"/> matrix B of right hand side vectors, stored columnwise.</param>
        /// <param name="s">Contains the singular values of matrix A in decreasing order on exit; dimension at least min(<paramref name="m"/>, <paramref name="n"/>).</param>
        /// <param name="rcond">Is used to determine the effective rank of A. Singular values s_i &lt;= rcond * s_1 are treated as zero.</param>
        /// <param name="work">A workspace array.</param>
        /// <param name="rwork">A workspace array with dimension at least 5 * min(<paramref name="m"/>, <paramref name="n"/>).</param>
        /// <param name="rank">The effective rank of matrix A, that is, the number of singular values which are reater than <paramref name="rcond"/> * s_1 (output).</param>
        public void driver_zgelss(int m, int n, int nrhs, Complex[] a, Complex[] b, double[] s, double rcond, Complex[] work, double[] rwork, out int rank)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = work.Length;

            _driver_zgelss(ref m, ref n, ref nrhs, a, ref lda, b, ref ldb, s, ref rcond, out rank, work, ref lwork, rwork, out info);
            LapackNativeWrapper.CheckForError(info, "zgelss");
        }

        /// <summary>Gets a optimal workspace array length for the <c>dgelsd</c> function.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="rcond">Is used to determine the effective rank of A. Singular values s_i &lt;= rcond * s_1 are treated as zero.</param>
        /// <param name="ilwork">The optimal workspace array length for parameter 'iwork'.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_dgelsdQuery(int m, int n, int nrhs, double rcond, out int ilwork)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = -1;
            int rank;

            unsafe
            {
                double* work = stackalloc double[1];
                int* iwork = stackalloc int[1];

                _driver_dgelsd(ref m, ref n, ref nrhs, null, ref lda, null, ref ldb, null, ref rcond, out rank, work, ref lwork, iwork, out info);

                LapackNativeWrapper.CheckForError(info, "dgelsd");

                ilwork = iwork[0];
                return ((int)work[0]) + 1;
            }
        }

        ///<summary>Computes the minimum-norm solution to a linear least squares problem using the singular value decomposition of A and a divide and conquer method, i.e. minimize ||b - A * x||.</summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="a">The <paramref name="m"/>-by-<paramref name="n"/> matrix provided column-by-column.</param>
        /// <param name="b">The <paramref name="m"/>-by-<paramref name="nrhs"/> matrix B of right hand side vectors, stored columnwise.</param>
        /// <param name="s">Contains the singular values of matrix A in decreasing order on exit; dimension at least min(<paramref name="m"/>, <paramref name="n"/>).</param>
        /// <param name="rcond">Is used to determine the effective rank of A. Singular values s_i &lt;= rcond * s_1 are treated as zero.</param>
        /// <param name="work">A workspace array.</param>
        /// <param name="iwork">A workspace array.</param>
        /// <param name="rank">The effective rank of matrix A, that is, the number of singular values which are reater than <paramref name="rcond"/> * s_1 (output).</param>
        public void driver_dgelsd(int m, int n, int nrhs, double[] a, double[] b, double[] s, double rcond, double[] work, int[] iwork, out int rank)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = work.Length;

            _driver_dgelsd(ref m, ref n, ref nrhs, a, ref lda, b, ref ldb, s, ref rcond, out rank, work, ref lwork, iwork, out info);
            LapackNativeWrapper.CheckForError(info, "dgelsd");
        }

        /// <summary>Gets a optimal workspace array length for the <c>zgelsd</c> function.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="rcond">Is used to determine the effective rank of A. Singular values s_i &lt;= rcond * s_1 are treated as zero.</param>
        /// <param name="ilwork">The optimal workspace array length for parameter 'iwork'.</param>
        /// <param name="rlwork">The optimal workspace array length for parameter 'rwork'.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_zgelsdQuery(int m, int n, int nrhs, double rcond, out int ilwork, out int rlwork)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = -1;
            int rank;

            unsafe
            {
                Complex* work = stackalloc Complex[1];
                int* iwork = stackalloc int[1];
                double* rwork = stackalloc double[1];

                _driver_zgelsd(ref m, ref n, ref nrhs, null, ref lda, null, ref ldb, null, ref rcond, out rank, work, ref lwork, rwork, iwork, out info);

                LapackNativeWrapper.CheckForError(info, "zgelsd");

                ilwork = iwork[0];
                rlwork = ((int)rwork[0]) + 1;
                return ((int)work[0].Real) + 1;
            }
        }

        ///<summary>Computes the minimum-norm solution to a linear least squares problem using the singular value decomposition of A and a divide and conquer method, i.e. minimize ||b - A * x||.</summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrix A.</param>
        /// <param name="nrhs">The number of right-hand sides; the number of columns in b.</param>
        /// <param name="a">The <paramref name="m"/>-by-<paramref name="n"/> matrix provided column-by-column.</param>
        /// <param name="b">The <paramref name="m"/>-by-<paramref name="nrhs"/> matrix B of right hand side vectors, stored columnwise.</param>
        /// <param name="s">Contains the singular values of matrix A in decreasing order on exit; dimension at least min(<paramref name="m"/>, <paramref name="n"/>).</param>
        /// <param name="rcond">Is used to determine the effective rank of A. Singular values s_i &lt;= rcond * s_1 are treated as zero.</param>
        /// <param name="work">A workspace array.</param>
        /// <param name="rwork">A workspace array.</param>
        /// <param name="iwork">A workspace array.</param>
        /// <param name="rank">The effective rank of matrix A, that is, the number of singular values which are reater than <paramref name="rcond"/> * s_1 (output).</param>
        public void driver_zgelsd(int m, int n, int nrhs, Complex[] a, Complex[] b, double[] s, double rcond, Complex[] work, double[] rwork, int[] iwork, out int rank)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, Math.Max(m, n));
            int lwork = work.Length;

            _driver_zgelsd(ref m, ref n, ref nrhs, a, ref lda, b, ref ldb, s, ref rcond, out rank, work, ref lwork, rwork, iwork, out info);
            LapackNativeWrapper.CheckForError(info, "zgelsd");
        }

        /// <summary>Gets a optimal workspace array length for the <c>dgglse</c> function.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrices A and B.</param>
        /// <param name="p">The number of rows of the matrix B.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_dgglseQuery(int m, int n, int p)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, p);
            int lwork = -1;

            unsafe
            {
                double* work = stackalloc double[1];

                _driver_dgglse(ref m, ref n, ref p, null, ref lda, null, ref ldb, null, null, null, work, ref lwork, out info);
                LapackNativeWrapper.CheckForError(info, "dgelss");

                return ((int)work[0]) + 1;
            }
        }

        /// <summary>Solves the linear equality-constrained least squares problem using a generalized RQ factorization, i.e. minimize ||c-A*x||^2 subject to B*x=d.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrices A and B.</param>
        /// <param name="p">The number of rows of the matrix B.</param>
        /// <param name="a">The <paramref name="m"/>-by-<paramref name="n"/> matrix provided column-by-column. On exist, the elements on and above the diagonal of the array contain the min(m,n)-by-n upper trapezoid matrix T.</param>
        /// <param name="b">The <paramref name="p"/>-by-<paramref name="n"/> matrix B of right hand side vectors, stored columnwise. On exit, the upper triangle of the subarray B</param>
        /// <param name="c">The right hand side vector for the least squares part of the LSE problem; dimension at least <paramref name="m"/>.</param>
        /// <param name="d">The right hand side vector for the constrained equation; dimension at least <paramref name="p"/>.</param>
        /// <param name="x">The solution of the LSE problem; dimension at least <paramref name="n"/>.</param>
        /// <param name="work">A workspace array.</param>
        public void driver_dgglse(int m, int n, int p, double[] a, double[] b, double[] c, double[] d, double[] x, double[] work)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, p);
            int lwork = work.Length;

            _driver_dgglse(ref m, ref n, ref p, a, ref lda, b, ref ldb, c, d, x, work, ref lwork, out info);
            LapackNativeWrapper.CheckForError(info, "dgglse");
        }

        /// <summary>Gets a optimal workspace array length for the <c>zgglse</c> function.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrices A and B.</param>
        /// <param name="p">The number of rows of the matrix B.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_zgglseQuery(int m, int n, int p)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, p);
            int lwork = -1;

            unsafe
            {
                Complex* work = stackalloc Complex[1];

                _driver_zgglse(ref m, ref n, ref p, null, ref lda, null, ref ldb, null, null, null, work, ref lwork, out info);
                LapackNativeWrapper.CheckForError(info, "zgelss");

                return ((int)work[0].Real) + 1;
            }
        }

        /// <summary>Solves the linear equality-constrained least squares problem using a generalized RQ factorization, i.e. minimize ||c-A*x||^2 subject to B*x=d.
        /// </summary>
        /// <param name="m">The number of rows of the matrix A.</param>
        /// <param name="n">The number of columns of the matrices A and B.</param>
        /// <param name="p">The number of rows of the matrix B.</param>
        /// <param name="a">The <paramref name="m"/>-by-<paramref name="n"/> matrix provided column-by-column. On exist, the elements on and above the diagonal of the array contain the min(m,n)-by-n upper trapezoid matrix T.</param>
        /// <param name="b">The <paramref name="p"/>-by-<paramref name="n"/> matrix B of right hand side vectors, stored columnwise. On exit, the upper triangle of the subarray B</param>
        /// <param name="c">The right hand side vector for the least squares part of the LSE problem; dimension at least <paramref name="m"/>.</param>
        /// <param name="d">The right hand side vector for the constrained equation; dimension at least <paramref name="p"/>.</param>
        /// <param name="x">The solution of the LSE problem; dimension at least <paramref name="n"/>.</param>
        /// <param name="work">A workspace array.</param>
        public void driver_zgglse(int m, int n, int p, Complex[] a, Complex[] b, Complex[] c, Complex[] d, Complex[] x, Complex[] work)
        {
            int info;

            int lda = Math.Max(1, m);
            int ldb = Math.Max(1, p);
            int lwork = work.Length;

            _driver_zgglse(ref m, ref n, ref p, a, ref lda, b, ref ldb, c, d, x, work, ref lwork, out info);
            LapackNativeWrapper.CheckForError(info, "zgglse");
        }

        /// <summary>Gets a optimal workspace array length for the <c>dggglm</c> function.
        /// </summary>
        /// <param name="m">The number of rows in the matrices A and B.</param>
        /// <param name="n">The number of columns in matrix A.</param>
        /// <param name="p">The number of columns in matrix B.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_dggglmQuery(int n, int m, int p)
        {
            int info;

            int lda = Math.Max(1, n);
            int ldb = Math.Max(1, n);
            int lwork = -1;

            unsafe
            {
                double* work = stackalloc double[1];

                _driver_dggglm(ref n, ref m, ref p, null, ref lda, null, ref ldb, null, null, null, work, ref lwork, out info);
                LapackNativeWrapper.CheckForError(info, "dggglm");

                return ((int)work[0]) + 1;
            }
        }

        /// <summary>Solves a general Gauss-Markov linear model problem using a generalized QR factorization, i.e. minimize ||y|| subject to d = A * x + B * y.
        /// </summary>
        /// <param name="m">The number of rows in the matrices A and B.</param>
        /// <param name="n">The number of columns in matrix A.</param>
        /// <param name="p">The number of columns in matrix B.</param>
        /// <param name="a">The <paramref name="n"/>-by-<paramref name="m"/> matrix A provided column-by-column.</param>
        /// <param name="b">The <paramref name="n"/>-by-<paramref name="p"/> matrix B provided column-by-column.</param>
        /// <param name="d">The left hand side vector for the constrained equation; dimension at least <paramref name="n"/>.</param>
        /// <param name="x">The solution x of the LSE problem; dimension at least <paramref name="m"/>.</param>
        /// <param name="y">The solution y of the LSE problem; dimension at least <paramref name="p"/>.</param>
        /// <param name="work">A workspace array.</param>
        public void driver_dggglm(int n, int m, int p, double[] a, double[] b, double[] d, double[] x, double[] y, double[] work)
        {
            int info;

            int lda = Math.Max(1, n);
            int ldb = Math.Max(1, n);
            int lwork = work.Length;

            _driver_dggglm(ref n, ref m, ref p, a, ref lda, b, ref ldb, d, x, y, work, ref lwork, out info);
            LapackNativeWrapper.CheckForError(info, "dggglm");
        }

        /// <summary>Gets a optimal workspace array length for the <c>zggglm</c> function.
        /// </summary>
        /// <param name="m">The number of rows in the matrices A and B.</param>
        /// <param name="n">The number of columns in matrix A.</param>
        /// <param name="p">The number of columns in matrix B.</param>
        /// <returns>The optimal workspace array length.</returns>
        public int driver_zggglmQuery(int n, int m, int p)
        {
            int info;

            int lda = Math.Max(1, n);
            int ldb = Math.Max(1, n);
            int lwork = -1;

            unsafe
            {
                Complex* work = stackalloc Complex[1];

                _driver_zggglm(ref n, ref m, ref p, null, ref lda, null, ref ldb, null, null, null, work, ref lwork, out info);
                LapackNativeWrapper.CheckForError(info, "zggglm");

                return ((int)work[0].Real) + 1;
            }
        }

        /// <summary>Solves a general Gauss-Markov linear model problem using a generalized QR factorization, i.e. minimize ||y|| subject to d = A * x + B * y.
        /// </summary>
        /// <param name="m">The number of rows in the matrices A and B.</param>
        /// <param name="n">The number of columns in matrix A.</param>
        /// <param name="p">The number of columns in matrix B.</param>
        /// <param name="a">The <paramref name="n"/>-by-<paramref name="m"/> matrix A provided column-by-column.</param>
        /// <param name="b">The <paramref name="n"/>-by-<paramref name="p"/> matrix B provided column-by-column.</param>
        /// <param name="d">The left hand side vector for the constrained equation; dimension at least <paramref name="n"/>.</param>
        /// <param name="x">The solution x of the LSE problem; dimension at least <paramref name="m"/>.</param>
        /// <param name="y">The solution y of the LSE problem; dimension at least <paramref name="p"/>.</param>
        /// <param name="work">A workspace array.</param>
        public void driver_zggglm(int n, int m, int p, Complex[] a, Complex[] b, Complex[] d, Complex[] x, Complex[] y, Complex[] work)
        {
            int info;

            int lda = Math.Max(1, n);
            int ldb = Math.Max(1, n);
            int lwork = work.Length;

            _driver_zggglm(ref n, ref m, ref p, a, ref lda, b, ref ldb, d, x, y, work, ref lwork, out info);
            LapackNativeWrapper.CheckForError(info, "zggglm");
        }
        #endregion

        #region internal static methods

        /// <summary>Creates a new <see cref="LinearLeastSquaresProblems"/> instance.
        /// </summary>
        /// <returns>A new <see cref="LinearLeastSquaresProblems"/> instance.</returns>
        internal static LinearLeastSquaresProblems Create() => new LinearLeastSquaresProblems();
        #endregion
    }
}