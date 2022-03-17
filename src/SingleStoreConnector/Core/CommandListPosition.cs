namespace SingleStoreConnector.Core;

/// <summary>
/// <see cref="CommandListPosition"/> encapsulates a list of <see cref="ISingleStoreCommand"/> and the current position within that list.
/// </summary>
internal struct CommandListPosition
{
	public CommandListPosition(IReadOnlyList<ISingleStoreCommand> commands)
	{
		Commands = commands;
		CommandIndex = 0;
		PreparedStatementIndex = 0;
	}

	/// <summary>
	/// The commands in the list.
	/// </summary>
	public IReadOnlyList<ISingleStoreCommand> Commands { get; }

	/// <summary>
	/// The index of the current command.
	/// </summary>
	public int CommandIndex;

	/// <summary>
	/// If the current command is a prepared statement, the index of the current prepared statement for that command.
	/// </summary>
	public int PreparedStatementIndex;
}
