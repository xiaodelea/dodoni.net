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

using Dodoni.BasicComponents;

namespace Dodoni.MathLibrary.ProbabilityTheory
{
    public abstract partial class PseudoSqrtMatrixDecomposer
    {
        /// <summary>Serves as abstract workspace container for a correlation matrix decomposition, i.e. a container class that contains specific working arrays etc. to remove memory
        /// allocation in the case of a frequently recurring operation.
        /// </summary>
        public abstract class WorkspaceContainer
        {
            /// <summary>Initializes a new instance of the <see cref="WorkspaceContainer" /> class.
            /// </summary>
            /// <param name="dimension">The dimension of correlation matrices for which the workspace is suited.</param>
            protected WorkspaceContainer(int dimension)
            {
                if (dimension < 1)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.ArgumentOutOfRangeGreaterEqual, dimension, 1), nameof(dimension));
                }
                Dimension = dimension;
            }

            /// <summary>Gets the dimension of correlation matrices for which the workspace is suited.
            /// </summary>
            /// <value>The dimension of correlation matrices for which the workspace is suited.</value>
            public int Dimension
            {
                get;
                private set;
            }
        }
    }
}