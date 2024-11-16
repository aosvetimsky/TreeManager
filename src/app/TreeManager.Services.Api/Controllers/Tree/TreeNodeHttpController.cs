using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreeManager.Services.Api.Controllers.Tree.Transport;
using TreeManager.Contracts.Tree;

namespace TreeManager.Services.Api.Controllers.Tree
{
    [Route("tree/node")]
    public class TreeNodeController : ApiControllerBase
    {
        private readonly ITreeAppService _treeAppService;
        private readonly TreeMapper _treeMapper;

        public TreeNodeController(ITreeAppService treeAppService, TreeMapper treeMapper)
        {
            _treeAppService = treeAppService;
            _treeMapper = treeMapper;
        }

        [HttpPost]
        [Route("{treeName}")]
        public async Task<IResult> Create(string treeName, [FromBody] CreateTreeNodeRequest request)
        {
            var tree = await _treeAppService.FindTree(treeName);

            if (tree == null)
            {
                return Results.NotFound();
            }

            var result = await _treeAppService.CreateTreeNode(treeName, request.ParentTreeNodeId, request.TreeNodeName);

            if (!result.IsSucceeded)
            {
                return HandleOperationInvalidResult(result.Error);
            }

            return Results.Ok();
        }

        [HttpPut]
        [Route("{treeName}")]
        public async Task<IResult> Rename(string treeName, [FromBody] RenameTreeNodeRequest request)
        {
            var tree = await _treeAppService.FindTree(treeName);

            if (tree == null)
            {
                return Results.NotFound();
            }

            var result = await _treeAppService.UpdateTreeNode(treeName, request.TreeNodeId, request.TreeNodeName);

            if (!result.IsSucceeded)
            {
                return HandleOperationInvalidResult(result.Error);
            }

            return Results.Ok();
        }

        [HttpDelete]
        [Route("{treeName}")]
        public async Task<IResult> Delete(string treeName, [FromBody] DeleteTreeNodeRequest request)
        {
            var tree = await _treeAppService.FindTree(treeName);

            if (tree == null)
            {
                return Results.NotFound();
            }

            var result = await _treeAppService.DeleteTreeNode(treeName, request.TreeNodeId);

            if (!result.IsSucceeded)
            {
                return HandleOperationInvalidResult(result.Error);
            }

            return Results.Ok();
        }
    }
}
