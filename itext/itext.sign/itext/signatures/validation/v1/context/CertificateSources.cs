using System;
using iText.Commons.Utils.Collections;
using iText.Signatures.Validation.V1.Context;

namespace iText.Signatures.Validation.V1.Context {
    /// <summary>
    /// Container class, which contains set of single
    /// <see cref="CertificateSource"/>
    /// values.
    /// </summary>
    public class CertificateSources {
        private readonly EnumSet<CertificateSource> set;

        private CertificateSources(EnumSet<CertificateSource> set) {
            this.set = set;
        }

        /// <summary>
        /// Creates
        /// <see cref="CertificateSources"/>
        /// container from several
        /// <see cref="CertificateSource"/>
        /// values.
        /// </summary>
        /// <param name="first">an element that the set is to contain initially</param>
        /// <param name="rest">the remaining elements the set is to contain</param>
        /// <returns>
        /// 
        /// <see cref="CertificateSources"/>
        /// container, containing provided elements
        /// </returns>
        public static CertificateSources Of(CertificateSource first
            , params CertificateSource[] rest) {
            return new CertificateSources(EnumSet<CertificateSource>.Of<CertificateSource
                >(first, rest));
        }

        /// <summary>
        /// Creates
        /// <see cref="CertificateSources"/>
        /// containing all
        /// <see cref="CertificateSource"/>
        /// values.
        /// </summary>
        /// <returns>
        /// 
        /// <see cref="CertificateSources"/>
        /// container containing all
        /// <see cref="CertificateSource"/>
        /// values
        /// </returns>
        public static CertificateSources All() {
            return new CertificateSources(EnumSet<CertificateSource>.AllOf<CertificateSource>());
        }

        /// <summary>
        /// Creates
        /// <see cref="CertificateSources"/>
        /// containing all the elements of this type
        /// that are not contained in the specified set.
        /// </summary>
        /// <param name="other">
        /// another
        /// <see cref="CertificateSources"/>
        /// from whose complement to initialize this container
        /// </param>
        /// <returns>
        /// the complement of the specified
        /// <see cref="CertificateSources"/>.
        /// </returns>
        public static CertificateSources ComplementOf(CertificateSources
             other) {
            EnumSet<CertificateSource> result = EnumSet<CertificateSource>.ComplementOf<CertificateSource>(other.set);
            if (result.IsEmpty()) {
                throw new ArgumentException("CertificateSources all has no valid complement.");
            }
            return new CertificateSources(result);
        }

        /// <summary>
        /// Gets encapsulated
        /// <see cref="EnumSet{T}"/>
        /// containing
        /// <see cref="CertificateSource"/>
        /// elements.
        /// </summary>
        /// <returns>
        /// encapsulated
        /// <see cref="EnumSet{T}"/>
        /// containing
        /// <see cref="CertificateSource"/>
        /// elements
        /// </returns>
        public virtual EnumSet<CertificateSource> GetSet() {
            return set;
        }
    }
}
