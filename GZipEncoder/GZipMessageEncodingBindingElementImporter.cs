using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Xml;

namespace Microsoft.Samples.GZipEncoder
{
    public class GZipMessageEncodingBindingElementImporter : IPolicyImportExtension
    {
        void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
        {
            if (importer == null)
            {
                throw new ArgumentNullException(nameof(importer));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            ICollection<XmlElement> assertions = context.GetBindingAssertions();
            foreach (XmlElement assertion in assertions)
            {
                if (assertion.NamespaceURI != GZipMessageEncodingPolicyConstants.GZipEncodingNamespace || assertion.LocalName != GZipMessageEncodingPolicyConstants.GZipEncodingName) continue;

                assertions.Remove(assertion);
                context.BindingElements.Add(new GZipMessageEncodingBindingElement());
                break;
            }
        }
    }
}