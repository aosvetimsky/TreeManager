using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreeManager.Services.Api.Controllers.Tree.Transport;
using TreeManager.Contracts.Tree;

namespace TreeManager.Services.Api.Controllers.Tree
{
    [Route("tree")]
    public class TreeController : ApiControllerBase
    {
        private readonly ITreeAppService _treeAppService;
        private readonly TreeMapper _treeMapper;

        public TreeController(ITreeAppService treeAppService, TreeMapper treeMapper)
        {
            _treeAppService = treeAppService;
            _treeMapper = treeMapper;
        }

        [HttpGet]
        [Route("{treeName}")]
        public async Task<IResult> Get(string treeName)
        {
            var tree = await _treeAppService.FindTree(treeName);

            if (tree == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(_treeMapper.ToTreeTransport(tree));
        }

        [HttpPost]
        public async Task<IResult> Create(CreateTreeRequest request)
        {
            var treeDto = _treeMapper.ToTreeDto(request.Tree);

            var result = await _treeAppService.CreateTree(treeDto);

            if (!result.IsSucceeded)
            {
                return HandleOperationInvalidResult(result.Error);
            }

            var tree = await _treeAppService.FindTree(result.Result);

            return Results.Ok(_treeMapper.ToTreeTransport(tree));
        }
    }
}
