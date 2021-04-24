import React, { useEffect, useState } from 'react'
import { remove } from "../api/task";
import { getAll } from "../api/shortTask";

export const deleteButtonHandler = async (id, removeFunc) => {
    let answer = window.confirm('Вы уверены?')
    if (answer) await removeFunc(id)

    return answer;
}

const Message = ({ props }) => {
    return (
        <tr>
            <td colSpan={props.columnsNumber}>{props.text}</td>
        </tr>
    );
}

function ShortTasksTable() {
    const numberOfColumns = 5;

    const [tasks, setTasks] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        async function initializeState() {
            let result = await getAll();
            if (!result) setIsLoading(false)
            else {
                setTasks(result)
                setIsLoading(false)
            }
        }

        initializeState();
    }, [])

    return (
        <div>
            <h1>Доступные задачи</h1>

            <table>
                <thead>
                    <tr>
                        <th>№</th>
                        <th>Краткое содержание</th>
                        <th>Исполнитель</th>
                        <th>Статус</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {tasks.length > 0 ? (
                        tasks.map(shortTask =>
                            <tr key={shortTask.id}>
                                <td>{shortTask.id}</td>
                                <td><a href={`task/${shortTask.id}`}>{shortTask.summary}</a></td>
                                <td>
                                    <a href={`employee/${shortTask.performingBy.id}`}>
                                        {`${shortTask.performingBy.firstName} ${shortTask.performingBy.lastName}`}
                                    </a>
                                </td>
                                <td>{shortTask.status.name}</td>
                                <td>
                                    <button onClick={async () => {
                                        setTasks(tasks.filter(item => item.id !== shortTask.id))
                                        await deleteButtonHandler(shortTask.id, remove);
                                    }
                                    }>Удалить</button>
                                </td>
                            </tr>
                        )
                    ) : (
                        isLoading
                            ? <Message props={{
                                text: "Загрузка...",
                                columnsNumber: numberOfColumns
                            }} />
                            : <Message props={{
                                text: "В настоящее время нет доступных задач",
                                columnsNumber: numberOfColumns
                            }} />
                    )}
                </tbody>
            </table >
        </div >
    );
}

export default ShortTasksTable;