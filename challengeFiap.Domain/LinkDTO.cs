using System;
using System.Collections.Generic;
using System.Text;

namespace challengeFiap.Domain
{
    public class LinkDTO
    {
        //endereco do endpoint 
        public string Href { get; set; }
        //finaidade
        public string Rel { get; set; }
        //metodo da http
        public string Method { get; set; }

        public LinkDTO(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
