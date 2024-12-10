﻿using KanbanApp.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace KanbanApp.DataAccess.Repositories
{
	public class TaskKanbanRepository : ITasksKanbanRepository
	{
		private readonly KanbanAppDbContext _context;

		public TaskKanbanRepository(KanbanAppDbContext context)
		{
			_context = context;
		}

		// Получение всех задач
		public async Task<List<TaskKanban>> Get()
		{
			var taskEntities = await _context.Tasks
				.AsNoTracking()
				.ToListAsync();

			var tasks = taskEntities
				.Select(b => TaskKanban.Create(b.Id, b.Name, b.Description ?? string.Empty, b.Priority, b.ColumnId, b.AssignedUserId).TaskKanban)
				.ToList();

			return tasks;
		}

		// Получение задач по колонке
		public async Task<List<TaskKanban>> GetByColumnId(Guid columnId)
		{
			var taskEntities = await _context.Tasks
				.AsNoTracking()
				.Where(b => b.ColumnId == columnId)  // Фильтруем задачи по ColumnId
				.ToListAsync();  // Получаем задачи

			var tasks = taskEntities
				.Select(b => TaskKanban.Create(b.Id, b.Name, b.Description ?? string.Empty, b.Priority, b.ColumnId, b.AssignedUserId).TaskKanban)
				.ToList();

			return tasks;
		}

		// Создание задачи
		public async Task<Guid> Create(TaskKanban taskKanban)
		{
			var taskKanbanEntity = new TaskKanbanEntity
			{
				Id = taskKanban.Id,
				Name = taskKanban.Name,
				Priority = taskKanban.Priority,
				Description = taskKanban.Description,
				ColumnId = taskKanban.ColumnId,
				AssignedUserId = taskKanban.AssignedUserId
			};

			await _context.Tasks.AddAsync(taskKanbanEntity);
			await _context.SaveChangesAsync();

			return taskKanbanEntity.Id;
		}

		// Обновление задачи
		public async Task<Guid> Update(Guid id, string name, string priority, string description, Guid? assignedUserId, Guid columnId)
		{
			await _context.Tasks
				.Where(b => b.Id == id)
				.ExecuteUpdateAsync(s => s
					.SetProperty(b => b.Name, b => name)
					.SetProperty(b => b.AssignedUserId, b => assignedUserId)
					.SetProperty(b => b.Priority, b => priority)
					.SetProperty(b => b.Description, b => description)
					.SetProperty(b => b.ColumnId, b => columnId)
				);
			return id;
		}

		// Удаление задачи
		public async Task<Guid> Delete(Guid id)
		{
			await _context.Tasks
				.Where(b => b.Id == id)
				.ExecuteDeleteAsync();

			return id;
		}
	}
}