﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoManager.Application.Common.Interfaces;
using TodoManager.Domain.Entities;

namespace TodoManager.Application.TodoLists.Commands.CreateTodoList
{
    public partial class CreateTodoListCommand : IRequest<int>
    {
        public string Title { get; set; }

        public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
        {
            private readonly IApplicationDbContext context;

            public CreateTodoListCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
            {
                var entity = new TodoList
                {
                    Title = request.Title
                };

                context.TodoLists.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
