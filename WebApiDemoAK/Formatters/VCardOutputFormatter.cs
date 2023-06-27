using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiDemoAK.Dtos;

namespace WebApiDemoAK.Formatters
{
    public class VCardOutputFormatter : TextOutputFormatter
    {
        public VCardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response=context.HttpContext.Response;
            var stringBuilder = new StringBuilder();
            if(context.Object is List<ContactDTO> list)
            {
                foreach (var item in list)
                {
                    FormatVCard(stringBuilder, item);
                }
            }
            else
            {
                var contact = context.Object as ContactDTO;
                FormatVCard(stringBuilder, contact);
            }
            return response.WriteAsync(stringBuilder.ToString());
        }

        private void FormatVCard(StringBuilder stringBuilder, ContactDTO item)
        {
            stringBuilder.AppendLine("BEGIN:VCARD");
            stringBuilder.AppendLine("VERSION:2.1");
            stringBuilder.AppendLine($"FN:{item.Firstname}");
            stringBuilder.AppendLine($"LN:{item.Lastname}");
            stringBuilder.AppendLine($"PAN:{item.PAN}");
            stringBuilder.AppendLine($"UID:{item.Id}");
            stringBuilder.AppendLine($"END:VCARD");
        }

        protected override bool CanWriteType(Type? type)
        {
            if(typeof(ContactDTO).IsAssignableFrom(type) ||
               typeof(List<ContactDTO>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
    }
}
