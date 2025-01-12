using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlackNet.Interaction;
using SlackNet.Interaction.Experimental;

namespace SlackNet.Handlers
{
    public class CompositeWorkflowStepEditHandler : IAsyncWorkflowStepEditHandler, IComposedHandler<WorkflowStepEdit>
    {
        private readonly IEnumerable<IAsyncWorkflowStepEditHandler> _handlers;
        public CompositeWorkflowStepEditHandler(IEnumerable<IAsyncWorkflowStepEditHandler> handlers) => _handlers = handlers;

        public Task Handle(WorkflowStepEdit workflowStepEdit, Responder respond) => Task.WhenAll(_handlers.Select(h => h.Handle(workflowStepEdit, respond)));

        IEnumerable<object> IComposedHandler<WorkflowStepEdit>.InnerHandlers(WorkflowStepEdit request) => _handlers.SelectMany(h => h.InnerHandlers(request));
    }
}