using System;

public abstract class BaseTask
{
    /**
     * That Action calls when the task is finished and sends own status (it might be successful or not)
     */
    protected readonly Action<bool> ActionWhenTaskFinished;
    
    protected BaseTask(Action<bool> actionWhenTaskFinished)
    {
        ActionWhenTaskFinished = actionWhenTaskFinished;
    }

    /// <summary>
    ///  Called once when the task is starts
    /// </summary>
    public abstract void OnStart();
    
    /// <summary>
    /// Called every frame, if the current task is active
    /// </summary>
    public abstract void Execute();
}
