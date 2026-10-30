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
    public record PagedResponse<T>(IEnumerable<T> Items, int PageNumber, int PageSize, int TotalPages, int TotalRecords, List<LinkDTO> Links);
}
