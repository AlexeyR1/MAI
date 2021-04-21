import React, { useState, useCallback } from "react"
import { Formik, Field, Form, FieldArray } from "formik"
import { TextField, Select, MenuItem, TextareaAutosize } from "@material-ui/core";
import { getAll as getStatuses } from "../api/status";
import { getAll as getPriorities } from "../api/priority";
import { getAll as getEmployees } from "../api/shortEmployee";
import { add as addTask } from "../api/task";
import { useDropzone } from "react-dropzone";
// import { date } from "yup";

function DropDownMenu({ data, setData, getData, propName, legendName }) {
    return (
        <div>
            <div className="label">{legendName}</div>
            <Field
                name={propName}
                type="select"
                as={Select}
                onOpen={async () => {
                    if (propName !== 'status.id') setData(await getData())
                }}
            >
                {data.map(item =>
                    item.lastName == null
                        ? <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                        : <MenuItem key={item.id} value={item.id}>{`${item.firstName} ${item.lastName}`}</MenuItem>
                )}
            </Field>
        </div>
    );
}

function MyDropzone({ attachments, setAttachments }) {
    const MAX_SIZE = 1073741824; // 1 Гб

    const onDrop = useCallback((acceptedFiles) => {
        acceptedFiles.forEach(async (file) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);

            reader.onabort = () => console.warn('file reading was aborted')
            reader.onerror = () => console.error('file reading has failed')
            reader.onload = () => {
                setAttachments([...attachments, {
                    name: file.name,
                    data: btoa(reader.result)
                }]);
            }
        })

    }, [attachments, setAttachments])
    const { getRootProps, getInputProps } = useDropzone({
        onDrop,
        maxSize: MAX_SIZE
    })

    return (
        <div>
            <div className="label">Файлы к задаче</div>
            <div className="dropzone" {...getRootProps()}>
                <input {...getInputProps()} />
                <p>&nbsp;&nbsp;Поместите сюда Ваши файлы</p>
            </div>
            {/* {isNaN(attachments) && (<div className="label">Прикрепленные файлы</div>)} */}
            <div className="attachment-container">
                {attachments.map((attachment, index) => (
                    <div key={index}>
                        <a key={index} href={atob(attachment.data)} style={{ marginRight: '2%' }} download>{attachment.name}</a>
                        <button
                            className="muted-button"
                            onClick={() => {
                                setAttachments(attachments.filter(item => item !== attachment))
                            }}
                        >x</button>
                    </div>
                ))}
            </div>
        </div>
    )
}

function AddTaskForm() {
    const [initialForm] = useState({
        summary: "",
        status: {
            id: 1, // Значение по умолчанию
        },
        author: {
            id: 0
        },
        performingBy: {
            id: 0
        },
        priority: {
            id: 2 // Значение по умолчанию
        },
        problemDescription: "",
        problemAttachments: []
    });

    const [statuses, setStatuses] = useState([{
        id: 1,
        name: "К выполнению"
    }]);

    const [authors, setAuthors] = useState([{
        id: 0,
        firstName: "",
        lastName: ""
    }]);

    const [performers, setPerformers] = useState([{
        id: 0,
        firstName: "",
        lastName: ""
    }]);

    const [priorities, setPriorities] = useState([{
        id: 2,
        name: "Средний"
    }]);

    // Буфер для прикреплений
    const [attachments, setAttachments] = useState([])

    return (
        <div className="custom-container">
            <h1>Создание задачи</h1>
            <Formik
                initialValues={initialForm}
                onSubmit={async (data, { resetForm }) => {
                    // Данные заносятся не напрямую в форму, т.к. при большом их объеме
                    // Форма будет медленно отображать вносимые изменения
                    data.problemAttachments = attachments;

                    await addTask(data);
                    resetForm({});
                    setAttachments([]); // Очистка состояния
                }}>

                {({
                    values, // Текущее состояние формы
                    isSubmitting
                }) => (
                    <Form>
                        <div>
                            <div className="label">Краткое описание</div>
                            <Field
                                name="summary"
                                type="input"
                                placeholder="Введите текст"
                                as={TextField}
                                fullWidth
                            />
                        </div>
                        <div>
                            <DropDownMenu
                                data={statuses}
                                setData={setStatuses}
                                getData={getStatuses}
                                propName={"status.id"}
                                legendName="Статус"
                            />
                        </div>
                        <div>
                            <DropDownMenu
                                data={authors}
                                setData={setAuthors}
                                getData={getEmployees}
                                propName={"author.id"}
                                legendName="Автор"
                            />
                        </div>
                        <div>
                            <DropDownMenu
                                data={performers}
                                setData={setPerformers}
                                getData={getEmployees}
                                propName={"performingBy.id"}
                                legendName="Исполнитель"
                            />
                        </div>
                        <div>
                            <DropDownMenu
                                data={priorities}
                                setData={setPriorities}
                                getData={getPriorities}
                                propName={"priority.id"}
                                legendName="Приоритет"
                            />
                        </div>
                        <div>
                            <div className="label">Подробное описание</div>
                            <Field
                                name="problemDescription"
                                placeholder="Введите текст"
                                rowsMin={6}
                                as={TextareaAutosize}
                            />
                        </div>
                        <FieldArray name="problemAttachments">
                            <MyDropzone
                                attachments={attachments}
                                setAttachments={setAttachments}
                            />
                        </FieldArray>
                        <div>
                            <button type="submit" disabled={isSubmitting}>Создать</button>
                        </div>
                        {/* <pre>{JSON.stringify(values, null, 2)}</pre> */}
                    </Form>
                )}
            </Formik>
        </div>
    );
}

export default AddTaskForm;