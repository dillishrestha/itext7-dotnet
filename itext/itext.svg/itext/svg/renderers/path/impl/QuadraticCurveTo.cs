/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using iText.IO.Util;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.StyledXmlParser.Css.Util;
using iText.Svg.Exceptions;

namespace iText.Svg.Renderers.Path.Impl {
    public class QuadraticCurveTo : AbstractPathShape {
        private String[] coordinates;

        /*
        * Implements quadratic Bezier curveTo(Q) attribute of SVG's path element
        */
        /// <summary>Draws a quadratic Bezier curve from the current point to (x,y) using (x1,y1) as the control point
        ///     </summary>
        public override void Draw(PdfCanvas canvas) {
            float x1 = CssUtils.ParseAbsoluteLength(coordinates[0]);
            float y1 = CssUtils.ParseAbsoluteLength(coordinates[1]);
            float x = CssUtils.ParseAbsoluteLength(coordinates[2]);
            float y = CssUtils.ParseAbsoluteLength(coordinates[3]);
            canvas.CurveTo(x1, y1, x, y);
        }

        public override void SetCoordinates(String[] coordinates, Point startPoint) {
            // startPoint will be used when relative quadratic curve is implemented
            if (coordinates.Length == 0 || coordinates.Length % 4 != 0) {
                throw new ArgumentException(MessageFormatUtil.Format(SvgExceptionMessageConstant.QUADRATIC_CURVE_TO_EXPECTS_FOLLOWING_PARAMETERS_GOT_0
                    , JavaUtil.ArraysToString(coordinates)));
            }
            if (coordinates.Length > 4) {
                // (x1 y1 x y)+ parameters will be implemented in the future
                throw new NotSupportedException();
            }
            else {
                this.coordinates = new String[] { coordinates[0], coordinates[1], coordinates[2], coordinates[3] };
            }
        }

        public override Point GetEndingPoint() {
            return CreatePoint(coordinates[2], coordinates[3]);
        }
    }
}
