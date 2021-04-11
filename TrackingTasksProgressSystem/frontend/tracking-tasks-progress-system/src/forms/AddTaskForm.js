import React, { useState, useCallback } from "react"
import { Formik, Field, Form, FieldArray } from "formik"
import { Button, TextField, Select, MenuItem, TextareaAutosize } from "@material-ui/core";
import { getAll as getStatuses } from "../api/status";
import { getAll as getPriorities } from "../api/priority";
import { getAll as getEmployees } from "../api/shortEmployee";
import { add as addTask } from "../api/task";
import { useDropzone } from "react-dropzone";
// import { date } from "yup";

function DropDownMenu({ data, setData, getData, propName }) {
    return (
        <div>
            <Field
                name={propName}
                type="select"
                as={Select}
                onOpen={async () => setData(await getData())}
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
            <div {...getRootProps()}>
                <input {...getInputProps()} />
                <p>Поместите сюда Ваши файлы</p>
            </div>
            {isNaN(attachments) && (<h4>Прикрепленные файлы:</h4>)}
            {attachments.map((attachment, index) => (
                <p key={index}>
                    <a key={index} href={attachment.data} download>{attachment.name}</a>
                </p>
            ))}
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
        problemAnnotation: "",
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
        <div>
            <h1>Добавить задачу</h1>
            <Formik
                initialValues={initialForm}
                onSubmit={async (data, { setSubmitting, resetForm }) => {
                    setSubmitting(true);

                    // Данные заносятся не напрямую в форму, т.к. при большом их объеме
                    // Форма будет медленно отображать внесенные изменения
                    data.problemAttachments = attachments;

                    await addTask(data);
                    resetForm({});
                    setAttachments([]); // Очистка состояния
                    setSubmitting(false);
                }}>

                {({
                    values, // Текущее состояние формы
                    isSubmitting
                }) => (
                    <Form>
                        <div>
                            <Field
                                name="summary"
                                type="input"
                                placeholder="Краткое описание"
                                as={TextField}
                            />
                        </div>
                        <div>
                            <DropDownMenu
                                data={statuses}
                                setData={setStatuses}
                                getData={getStatuses}
                                propName={"status.id"}
                            />
                        </div>
                        <div>
                            <DropDownMenu
                                data={authors}
                                setData={setAuthors}
                                getData={getEmployees}
                                propName={"author.id"}
                            />
                        </div>
                        <div>
                            <DropDownMenu
                                data={performers}
                                setData={setPerformers}
                                getData={getEmployees}
                                propName={"performingBy.id"}
                            />
                        </div>
                        <div>
                            <DropDownMenu
                                data={priorities}
                                setData={setPriorities}
                                getData={getPriorities}
                                propName={"priority.id"}
                            />
                        </div>
                        <div>
                            <Field
                                name="problemAnnotation"
                                placeholder="Комментарий к задаче"
                                rowsMin={6}
                                as={TextareaAutosize} />
                        </div>
                        <FieldArray name="problemAttachments">
                            <MyDropzone
                                attachments={attachments}
                                setAttachments={setAttachments}
                            />
                        </FieldArray>
                        <div>
                            <Button type="submit" disabled={isSubmitting}>Добавить</Button>
                        </div>
                        <pre>{JSON.stringify(values, null, 2)}</pre>
                    </Form>
                )}
            </Formik>
        </div>
    );
}

export default AddTaskForm;