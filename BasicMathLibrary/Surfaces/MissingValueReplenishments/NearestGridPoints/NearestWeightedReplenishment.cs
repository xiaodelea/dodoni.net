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
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Dodoni.BasicComponents;
using Dodoni.MathLibrary.GridPointCurves;

namespace Dodoni.MathLibrary.Surfaces.MissingValueReplenishments
{
    /// <summary>Apply a interpolation along x-axis and y-axis to fill missing values, where the two nearest grid points are taken into account only.
    /// Use a weighted sum of both to fill missing values.
    /// </summary>
    internal class NearestWeightedReplenishment : LabelMatrix.MissingValueReplenishment
    {
        #region nested classes

        /// <summary>Serves as implementation of the missing value replenishment.
        /// </summary>
        private class Replenishment : IMissingValueReplenishment
        {
            #region private members

            /// <summary>The (curve) interpolator along x-axis in its <see cref="ICurveDataFitting"/> representation.
            /// </summary>
            private ICurveDataFitting m_HorizontalInterpolator;

            /// <summary>The (curve) interpolator along y-axis in its <see cref="ICurveDataFitting"/> representation.
            /// </summary>
            private ICurveDataFitting m_VerticalInterpolator;

            /// <summary>The weight for the convex combination of the interpolated values which are the result of a linear interpolation in horizontal and vertical direction.
            /// </summary>
            /// <remarks>The estimated value of a missing grid point is specified by (1.0 - Weight) * estimatedValueInXDirection + Weight * estimatedValueInYDirection.</remarks>
            private double m_Weight;
            #endregion

            #region internal constructors

            /// <summary>Initializes a new instance of the <see cref="Replenishment"/> class.
            /// </summary>
            /// <param name="horizontalInterpolator">The (curve) interpolator along x-axis.</param>
            /// <param name="verticalInterpolator"></param>
            /// <param name="weight">The weight for the convex combination of the interpolated values which are the result of a linear interpolation in horizontal and vertical direction.</param>
            internal Replenishment(GridPointCurve.Interpolator horizontalInterpolator, GridPointCurve.Interpolator verticalInterpolator, double weight)
            {
                m_HorizontalInterpolator = horizontalInterpolator.Create();
                m_VerticalInterpolator = verticalInterpolator.Create();
                m_Weight = weight;
            }
            #endregion

            #region IMissingValueReplenishment Members

            /// <summary>Fill missing values of a specified matrix with respect to the missing value replenishment represented by the current instance.
            /// </summary>
            /// <param name="rowCount">The number of rows.</param>
            /// <param name="columnCount">The number of columns.</param>
            /// <param name="dataMatrix">The data matrix to replenish, provided column-by-column, i.e. contains at least <paramref name="rowCount"/> * <paramref name="columnCount"/> elements (in-/output).</param>
            /// <param name="xAxisLabeling">The labels of the x-axis in its <see cref="System.Double"/> representation, i.e. at least <paramref name="columnCount"/> elements.</param>
            /// <param name="yAxisLabeling">The labels of the y-axis in its <see cref="System.Double"/> representation, i.e. at least <paramref name="rowCount"/> elements.</param>
            /// <returns>A collection of null-based indices of the row and column of grid points in <paramref name="dataMatrix"/> which have been changed; <c>null</c> otherwise.</returns>
            public IEnumerable<(int RowIndex, int ColumnIndex)> Replenish(int rowCount, int columnCount, IList<double> dataMatrix, IList<double> xAxisLabeling, IList<double> yAxisLabeling)
            {
                var replenishIndices = new List<(int RowIndex, int ColumnIndex)>();
                var estimatedValues = new List<double>();

                /* Do not store estimated values in the matrix yet to avoid that values which are already replenished taken into account for the interpolation: */
                double estimatedValueInXDirection, estimatedValueInYDirection;
                for (int j = 0; j < columnCount; j++)
                {
                    int offset = j * rowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        if (Double.IsNaN(dataMatrix[i + offset]))  // [i,j]
                        {
                            replenishIndices.Add((RowIndex: i, ColumnIndex: j));
                            estimatedValueInXDirection = WeightedNearestGridPointMissingValueReplenishment.GetInterpolatedValueAlongXAxis(i, j, dataMatrix, rowCount, columnCount, xAxisLabeling, m_HorizontalInterpolator);
                            estimatedValueInYDirection = WeightedNearestGridPointMissingValueReplenishment.GetInterpolatedValueAlongYAxis(i, j, dataMatrix, rowCount, yAxisLabeling, m_VerticalInterpolator);

                            estimatedValues.Add((1.0 - m_Weight) * estimatedValueInXDirection + m_Weight * estimatedValueInYDirection);
                        }
                    }
                }
                // Insert missing values, i.e. fill the missing values
                int k = 0;
                foreach (var position in replenishIndices)
                {
                    dataMatrix[position.RowIndex + position.ColumnIndex * rowCount] = estimatedValues[k++];
                }
                return replenishIndices;
            }
            #endregion
        }
        #endregion

        #region private members

        /// <summary>The (curve) interpolator along x-axis.
        /// </summary>
        private GridPointCurve.Interpolator m_HorizontalInterpolator;

        /// <summary>The (curve) interpolator along y-axis.
        /// </summary>
        private GridPointCurve.Interpolator m_VerticalInterpolator;

        /// <summary>The weight for the convex combination of the interpolated values which are the result of a linear interpolation in horizontal and vertical direction.
        /// </summary>
        /// <remarks>The estimated value of a missing grid point is specified by (1.0 - Weight) * estimatedValueInXDirection + Weight * estimatedValueInYDirection.</remarks>
        private double m_Weight;

        /// <summary>The name of the missing value replenishment.
        /// </summary>
        private IdentifierString m_Name;

        /// <summary>The long name of the missing value replenishment.
        /// </summary>
        private IdentifierString m_LongName;
        #endregion

        #region internal constructors

        /// <summary>Initializes a new instance of the <see cref="NearestWeightedReplenishment"/> class.
        /// </summary>
        /// <param name="horizontalInterpolator">The (curve) interpolator along x-axis.</param>
        /// <param name="verticalInterpolator">The (curve) interpolator along y-axis.</param>
        /// <param name="weight">The weight for the convex combination of the interpolated values which are the result of a linear interpolation in horizontal and vertical direction.</param>
        /// <remarks>The estimated value of a missing grid point is specified by (1.0 - Weight) * estimatedValueInXDirection + Weight * estimatedValueInYDirection.</remarks>
        internal NearestWeightedReplenishment(GridPointCurve.Interpolator horizontalInterpolator, GridPointCurve.Interpolator verticalInterpolator, double weight = 0.5)
            : base(MissingValueReplenishmentResource.AnnotationNearestWeighted)
        {
            m_HorizontalInterpolator = horizontalInterpolator ?? throw new ArgumentNullException(nameof(horizontalInterpolator));
            m_VerticalInterpolator = verticalInterpolator ?? throw new ArgumentNullException(nameof(verticalInterpolator));
            m_Name = new IdentifierString("WeightedInterpolation");
            m_LongName = new IdentifierString(String.Format(MissingValueReplenishmentResource.LongNameNearestWeighted, horizontalInterpolator.Name.String, verticalInterpolator.Name.String, weight));
            m_Weight = weight;
        }
        #endregion

        #region public methods

        /// <summary>Creates a <see cref="IMissingValueReplenishment"/> object that represents the implementation of the missing value replenishment.
        /// </summary>
        /// <returns>A <see cref="IMissingValueReplenishment"/> object that represents the implementation of the missing value replenishment.</returns>
        public override IMissingValueReplenishment Create()
        {
            return new Replenishment(m_HorizontalInterpolator, m_VerticalInterpolator, m_Weight);
        }
        #endregion

        #region protected methods

        /// <summary>Gets the name of the missing value replenishment.
        /// </summary>
        /// <returns>The name of the missing value replenishment.</returns>
        protected override IdentifierString GetName()
        {
            return m_Name;
        }

        /// <summary>Gets the long name of the missing value replenishment.
        /// </summary>
        /// <returns>The (perhaps) language dependent long name of the missing value replenishment.</returns>
        protected override IdentifierString GetLongName()
        {
            return m_LongName;
        }
        #endregion
    }
}
