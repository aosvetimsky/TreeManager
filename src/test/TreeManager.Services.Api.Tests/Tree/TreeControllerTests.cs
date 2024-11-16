using Xunit;
using System.Net;
using FluentAssertions;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TreeManager.Services.Api.Controllers.Tree.Transport;

namespace TreeManager.Services.Api.Tests.Tree
{
    public class TreeControllerTests : IClassFixture<Setup.ApiWebApplicationFactory>
    {
        readonly HttpClient _client;

        public TreeControllerTests(Setup.ApiWebApplicationFactory application)
        {
            _client = application.CreateClient();
        }

        [Fact]
        public async Task GET_not_existing_tree_returns_not_found()
        {
            var response = await _client.GetAsync("/tree/12345");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task POST_create_tree_returns_created_tree()
        {
            var tree = new Controllers.Tree.Transport.Tree
            {
                Name = "tree",
                Root = new TreeNode
                {
                    Name = "root",
                    Children = new List<TreeNode> 
                    {
                      new TreeNode{ Name = "child1" },
                      new TreeNode{ Name = "child2" }
                    }.ToArray()
                }
            };
            var response = await _client.PostAsJsonAsync("/tree", new CreateTreeRequest { Tree = tree });
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var createdTree = await response.Content.ReadFromJsonAsync<Controllers.Tree.Transport.Tree>();
            createdTree.Should().NotBeNull();
            createdTree.Name.Should().Be("tree");
            createdTree.Root.Should().NotBeNull();
            createdTree.Root.Children.Length.Should().Be(2);
        }
    }
}
