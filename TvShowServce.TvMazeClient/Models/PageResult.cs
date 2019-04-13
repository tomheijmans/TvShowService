using System;
using System.Collections.Generic;

namespace TvShowService.TvMazeClient.Models
{
    /// <summary>
    /// Result of a paged request
    /// </summary>
    public class PageResult<TModel>
    {
        /// <summary>
        /// Content of the requests page
        /// </summary>
        public IList<TModel> Content { get; }

        /// <summary>
        /// States if the given page exists
        /// </summary>
        public bool PageExist { get; }

        private PageResult(bool pageExists, IList<TModel> content)
        {
            PageExist = pageExists;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// Create a page does not exist object
        /// </summary>
        /// <returns></returns>
        public static PageResult<TModel> PageDoesNotExist()
        {
            return new PageResult<TModel>(false, new List<TModel>());
        }

        /// <summary>
        /// Creates an instance for a page that does exist with given content
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static PageResult<TModel> PageExistsWithContent(IList<TModel> content)
        {
            return new PageResult<TModel>(true, content);
        }
    }
}
