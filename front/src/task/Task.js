import Subtask from "../subtask/Subtask";
import API from "../API.js";
import "./style.css";

const Task = () => {
  async function getTask() {
    const URL = "http://localhost:5000";
    const JWT =
      "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3YjZkZjUwNC03NDc0LTQ5NmEtODY3Ni1jMmMzYzk0ODg2ZWMiLCJzdWIiOiIxIiwibmJmIjoxNjc2MjI0NDg4LCJleHAiOjE2NzYyMjgwODgsImlhdCI6MTY3NjIyNDQ4OCwiaXNzIjoidGFza2lsbC1hcGktZGV2ZWxvcG1lbnQiLCJhdWQiOiJ0YXNraWxsLWFwaS1kZXZlbG9wbWVudCJ9.JyotgqXtFztq0ljfMyR8m521fEHE8_IdIag1wD9F01c";
    const AUTH_HEADER = {
      headers: { Authentication: `Bearer ${JWT}` },
    };

    console.log(AUTH_HEADER);

    var response = await fetch("http://localhost:5000/tasks/1", AUTH_HEADER);
    console.log(response);
    return await response.json();
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
