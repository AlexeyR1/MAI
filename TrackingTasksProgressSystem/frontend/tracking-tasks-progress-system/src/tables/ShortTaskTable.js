import React, { useEffect, useState } from 'react'
import { remove } from "../api/task";
import { getAll } from "../api/shortTask";

const numberOfColumns = 5;

function deleteButtonHandler(id) {
    let answer = window.confirm('Вы уверены?')
    if (answer) remove(id)
}

function showMessage(message) {
    return (
        <tr>
            <td colSpan={numberOfColumns}>{message}</td>
        </tr>
    );
}

function ShortTaskTable() {
    const delay = 5;

    const [tasks, setTasks] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        async function initializeState() {
            setTasks(await getAll());
        }

        initializeState();
        setTimeout(() => {
            setIsLoading(false);
        }, delay * 1000);
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
                    </tr>
                </thead>
                <tbody>
                    {tasks.length > 0 ? (
                        tasks.map(shortTask =>
                            <tr key={shortTask.id}>
                                <td>{shortTask.id}</td>
                                <td><a href={`task/${shortTask.id}`}>{shortTask.summary}</a></td>
                                <td>{`${shortTask.performingBy.firstName} ${shortTask.performingBy.lastName}`}</td>
                                <td>{shortTask.status.name}</td>
                                <td>
                                    <button onClick={() => deleteButtonHandler(shortTask.id)}>Удалить</button>
                                </td>
                            </tr>
                        )
                    ) : (
                        isLoading ? showMessage("Загрузка...") : showMessage("В настоящее время нет доступных задач")
                    )}
                </tbody>
            </table >
        </div >
    );
}

export default ShortTaskTable;