import React, { useEffect, useState } from 'react'
import { remove } from "../api/task";
import { getAll } from "../api/shortTask";

export const deleteButtonHandler = async (id, removeFunc) => {
    let answer = window.confirm('Вы уверены?')
    if (answer) await removeFunc(id)
}

const showMessage = (message, numberOfColumns) => {
    return (
        <tr>
            <td colSpan={numberOfColumns}>{message}</td>
        </tr>
    );
}

function ShortTasksTable() {
    const numberOfColumns = 5;

    const [tasks, setTasks] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const delay = 2.5;

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
                            ? showMessage("Загрузка...", numberOfColumns)
                            : showMessage("В настоящее время нет доступных задач", numberOfColumns)
                    )}
                </tbody>
            </table >
        </div >
    );
}

export default ShortTasksTable;