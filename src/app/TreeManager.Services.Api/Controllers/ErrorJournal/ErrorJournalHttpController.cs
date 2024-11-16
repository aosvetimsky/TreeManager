using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TreeManager.Contracts.ErrorJournal;

namespace TreeManager.Services.Api.Controllers.ErrorJournal
{
    [Route("error-journal")]
    public class ErrorJournalController : ApiControllerBase
    {
        private readonly IErrorJournalAppService _errorJournalAppService;
        private readonly ErrorJournalMapper _errorJournalMapper;

        public ErrorJournalController(IErrorJournalAppService errorJournalAppService, ErrorJournalMapper errorJournalMapper)
        {
            _errorJournalAppService = errorJournalAppService;
            _errorJournalMapper = errorJournalMapper;
        }

        [HttpPost("search")]
        public async Task<SearchErrorJournalResponse> SearchRange(SearchErrorJournalRequest request)
        {
            var entries = await _errorJournalAppService.GetErrorEntriesRange(request.Skip, request.Take, request.Filter);

            return new SearchErrorJournalResponse
            {
                ErrorEntries = entries.Select(_errorJournalMapper.ToTransport).ToArray()
            };
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ErrorEntry>> Get(int id)
        {
            var entry = await _errorJournalAppService.GetErrorEntryById(id);

            if (entry == null)
            {
                return NotFound();
            }

            return _errorJournalMapper.ToTransport(entry);
        }
    }
}
