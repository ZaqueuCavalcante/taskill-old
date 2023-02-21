import Subtask from "../subtask/Subtask";
import getTaskById from "../API.js";
import "./style.css";

const Task = () => {
  async function getTask() {
    return await getTaskById(1);
  }

  return (
    <div id="task">
      <div id="task__title">Task title for test</div>
      <div id="task__description">
        This card will be show all informations about the task: title,
        description, project, subtasks...
      </div>
      <div id="task__subtasks">
        <Subtask title="Criar caixas de texto pro title e pra description"></Subtask>
        <Subtask title="Agrupar subtasks em um componente reutilizável"></Subtask>
        <Subtask title="Adicionar botões para project, subtasks, priority e labels"></Subtask>
        <Subtask title="Garantir responsividade do layout para nao quebrar"></Subtask>
      </div>
      <div id="footer">
        <button className="footer__info" onClick={getTask}>
          Front
        </button>
        <div className="footer__info">1/4</div>
        <button className="footer__info">High</button>
      </div>
    </div>
  );
};

export default Task;
