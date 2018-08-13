using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using BLL.Interfaces;
using BLL.DTO;
using TaskTrackingAPI.Models;
using BLL.Infrastructure;
using BLL.Services;

using Marvin.JsonPatch;
using System.Web.Http.Routing;
using System.Web;


namespace TaskTrackingAPI
{
    public static class Pagination
    {
        public static PagginationHeader GetPaginationHeader(int pageSize, int maxPageSize, int totalCount, int page, HttpRequestMessage requestMessage, string sort, string label)
        {
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

           
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(requestMessage);
            var prevLink = page > 1 ? urlHelper.Link("TasksList",
                new
                {
                    page = page - 1,
                    pageSize = pageSize,
                    sort = sort,
                    label = label
                        // userId = userId
                    }) : "";
            var nextLink = page < totalPages ? urlHelper.Link("TasksList",
                new
                {
                    page = page + 1,
                    pageSize = pageSize,
                    sort = sort,
                    label = label,
                        //userId = userId
                    }) : "";


            PagginationHeader paginationHeader = new PagginationHeader()
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                PreviousPageLink = prevLink,
                NextPageLink = nextLink
            };
            return paginationHeader;
        }
    }
}