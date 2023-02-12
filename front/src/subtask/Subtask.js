import React from "react";
import "./style.css";

const Subtask = ({ title }) => {
  const [isDone, setIsDone] = React.useState(false);

  return (
    <div id="subtask">
      <div id="subtask__title" onClick={() => setIsDone(!isDone)}>
        {isDone ? "[X]" : "[_]"} {title}
      </div>
    </div>
  );
};

export default Subtask;
