/*

This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

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
using System.Collections.Generic;
using iText.IO.Font;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Tagging;

namespace iText.Kernel.Pdf.Tagutils {
    public class AccessibilityProperties {
        protected internal String language;

        protected internal String actualText;

        protected internal String alternateDescription;

        protected internal String expansion;

        protected internal IList<PdfDictionary> attributesList = new List<PdfDictionary>();

        protected internal String phoneme;

        protected internal PdfName phoneticAlphabet;

        protected internal PdfNamespace @namespace;

        protected internal IList<TagTreePointer> refs = new List<TagTreePointer>();

        public virtual String GetLanguage() {
            return language;
        }

        public virtual AccessibilityProperties SetLanguage(String language) {
            this.language = language;
            return this;
        }

        public virtual String GetActualText() {
            return actualText;
        }

        public virtual AccessibilityProperties SetActualText(String actualText) {
            this.actualText = actualText;
            return this;
        }

        public virtual String GetAlternateDescription() {
            return alternateDescription;
        }

        public virtual AccessibilityProperties SetAlternateDescription(String alternateDescription) {
            this.alternateDescription = alternateDescription;
            return this;
        }

        public virtual String GetExpansion() {
            return expansion;
        }

        public virtual AccessibilityProperties SetExpansion(String expansion) {
            this.expansion = expansion;
            return this;
        }

        public virtual AccessibilityProperties AddAttributes(PdfDictionary attributes) {
            return AddAttributes(-1, attributes);
        }

        public virtual AccessibilityProperties AddAttributes(int index, PdfDictionary attributes) {
            if (attributes != null) {
                if (index > 0) {
                    attributesList.Add(index, attributes);
                }
                else {
                    attributesList.Add(attributes);
                }
            }
            return this;
        }

        public virtual AccessibilityProperties ClearAttributes() {
            attributesList.Clear();
            return this;
        }

        public virtual IList<PdfDictionary> GetAttributesList() {
            return attributesList;
        }

        public virtual AccessibilityProperties SetPhoneme(String phoneme) {
            this.phoneme = phoneme;
            return this;
        }

        public virtual String GetPhoneme() {
            return this.phoneme;
        }

        public virtual AccessibilityProperties SetPhoneticAlphabet(PdfName phoneticAlphabet) {
            this.phoneticAlphabet = phoneticAlphabet;
            return this;
        }

        public virtual PdfName GetPhoneticAlphabet() {
            return this.phoneticAlphabet;
        }

        public virtual AccessibilityProperties SetNamespace(PdfNamespace @namespace) {
            this.@namespace = @namespace;
            return this;
        }

        public virtual PdfNamespace GetNamespace() {
            return this.@namespace;
        }

        public virtual AccessibilityProperties AddRef(TagTreePointer treePointer) {
            refs.Add(new TagTreePointer(treePointer));
            return this;
        }

        public virtual IList<TagTreePointer> GetRefsList() {
            return JavaCollectionsUtil.UnmodifiableList(refs);
        }

        public virtual AccessibilityProperties ClearRefs() {
            refs.Clear();
            return this;
        }

        internal virtual void SetToStructElem(PdfStructElem elem) {
            if (GetActualText() != null) {
                elem.SetActualText(new PdfString(GetActualText(), PdfEncodings.UNICODE_BIG));
            }
            if (GetAlternateDescription() != null) {
                elem.SetAlt(new PdfString(GetAlternateDescription(), PdfEncodings.UNICODE_BIG));
            }
            if (GetExpansion() != null) {
                elem.SetE(new PdfString(GetExpansion(), PdfEncodings.UNICODE_BIG));
            }
            if (GetLanguage() != null) {
                elem.SetLang(new PdfString(GetLanguage(), PdfEncodings.UNICODE_BIG));
            }
            IList<PdfDictionary> newAttributesList = GetAttributesList();
            if (newAttributesList.Count > 0) {
                PdfObject attributesObject = elem.GetAttributes(false);
                PdfObject combinedAttributes = CombineAttributesList(attributesObject, -1, newAttributesList, elem.GetPdfObject
                    ().GetAsNumber(PdfName.R));
                elem.SetAttributes(combinedAttributes);
            }
            if (GetPhoneme() != null) {
                elem.SetPhoneme(new PdfString(GetPhoneme()));
            }
            if (GetPhoneticAlphabet() != null) {
                elem.SetPhoneticAlphabet(GetPhoneticAlphabet());
            }
            if (GetNamespace() != null) {
                elem.SetNamespace(GetNamespace());
            }
            foreach (TagTreePointer @ref in refs) {
                elem.AddRef(@ref.GetCurrentStructElem());
            }
        }

        [Obsolete]
        protected internal virtual PdfObject CombineAttributesList(PdfObject attributesObject, IList<PdfDictionary
            > newAttributesList, PdfNumber revision) {
            return CombineAttributesList(attributesObject, -1, newAttributesList, revision);
        }

        [Obsolete]
        protected internal virtual void AddNewAttributesToAttributesArray(IList<PdfDictionary> newAttributesList, 
            PdfNumber revision, PdfArray attributesArray) {
            AddNewAttributesToAttributesArray(-1, newAttributesList, revision, attributesArray);
        }

        protected internal static PdfObject CombineAttributesList(PdfObject attributesObject, int insertIndex, IList
            <PdfDictionary> newAttributesList, PdfNumber revision) {
            PdfObject combinedAttributes;
            if (attributesObject is PdfDictionary) {
                PdfArray combinedAttributesArray = new PdfArray();
                combinedAttributesArray.Add(attributesObject);
                AddNewAttributesToAttributesArray(insertIndex, newAttributesList, revision, combinedAttributesArray);
                combinedAttributes = combinedAttributesArray;
            }
            else {
                if (attributesObject is PdfArray) {
                    PdfArray combinedAttributesArray = (PdfArray)attributesObject;
                    AddNewAttributesToAttributesArray(insertIndex, newAttributesList, revision, combinedAttributesArray);
                    combinedAttributes = combinedAttributesArray;
                }
                else {
                    if (newAttributesList.Count == 1) {
                        if (insertIndex > 0) {
                            throw new IndexOutOfRangeException();
                        }
                        combinedAttributes = newAttributesList[0];
                    }
                    else {
                        combinedAttributes = new PdfArray();
                        AddNewAttributesToAttributesArray(insertIndex, newAttributesList, revision, (PdfArray)combinedAttributes);
                    }
                }
            }
            return combinedAttributes;
        }

        protected internal static void AddNewAttributesToAttributesArray(int insertIndex, IList<PdfDictionary> newAttributesList
            , PdfNumber revision, PdfArray attributesArray) {
            if (insertIndex < 0) {
                insertIndex = attributesArray.Size();
            }
            if (revision != null) {
                foreach (PdfDictionary attributes in newAttributesList) {
                    attributesArray.Add(insertIndex++, attributes);
                    attributesArray.Add(insertIndex++, revision);
                }
            }
            else {
                foreach (PdfDictionary newAttribute in newAttributesList) {
                    attributesArray.Add(insertIndex++, newAttribute);
                }
            }
        }
    }
}
