using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Domain.Repositories;
using FruTech.Backend.API.Tasks.Domain.Services;

namespace FruTech.Backend.API.Tasks.Application.Internal.CommandServices;

public class TaskCommandService : ITaskCommandService
{
    private readonly ITaskRepository _taskRepository;

    public TaskCommandService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Domain.Model.Aggregate.Task> Handle(CreateTaskCommand command)
    {
        var task = new Domain.Model.Aggregate.Task(
            command.Description,
            command.DueDate,
            command.FieldId
        )
        {
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = null
        };

        return await _taskRepository.CreateAsync(task);
    }

    public async Task<Domain.Model.Aggregate.Task?> Handle(EditTaskCommand command)
    {
        var existing = await _taskRepository.GetByIdAsync(command.Id);
        if (existing == null) return null;

        existing.Description = command.Description;
        existing.DueDate = command.DueDate;
        existing.UpdatedDate = DateTimeOffset.Now;

        return await _taskRepository.UpdateAsync(command.Id, existing);
    }

    public async Task<bool> Handle(DeleteTaskCommand command)
    {
        return await _taskRepository.DeleteAsync(command.Id);
    }
}
