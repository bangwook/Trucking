namespace Trucking.FSM
{
    abstract public class FSMState<T>
    {
        abstract public void Enter(T entity);

        virtual public void Execute(T entity)
        {
        }

        virtual public void Exit(T entity)
        {
        }

        virtual public void Push(T entity)
        {
        }

        virtual public void Pop(T entity)
        {
        }
    }
}