﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlackNet.Interaction;
using SlackNet.Interaction.Experimental;

namespace SlackNet.Handlers
{
    public class CompositeMessageShortcutHandler : IAsyncMessageShortcutHandler, IComposedHandler<MessageShortcut>
    {
        private readonly IEnumerable<IAsyncMessageShortcutHandler> _handlers;
        public CompositeMessageShortcutHandler(IEnumerable<IAsyncMessageShortcutHandler> handlers) => _handlers = handlers;

        public Task Handle(MessageShortcut request, Responder respond) => Task.WhenAll(_handlers.Select(h => h.Handle(request, respond)));

        IEnumerable<object> IComposedHandler<MessageShortcut>.InnerHandlers(MessageShortcut request) => _handlers.SelectMany(h => h.InnerHandlers(request));
    }
}