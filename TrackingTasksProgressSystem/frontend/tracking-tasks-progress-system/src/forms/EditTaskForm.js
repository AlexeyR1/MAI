import React, { useState, useEffect, useCallback } from "react";
import { Formik, Field, Form, FieldArray } from "formik"
import { useDropzone } from "react-dropzone";
import { useParams, useHistory } from "react-router-dom";
import { Button, TextField, Select, MenuItem, TextareaAutosize } from "@material-ui/core";
import CircularProgress from '@material-ui/core/CircularProgress';
import { getAll as getStatuses } from "../api/status";
import { getAll as getPriorities } from "../api/priority";
import { getAll as getEmployees } from "../api/shortEmployee";
import { getById, update, remove } from "../api/task";
import { deleteButtonHandler } from "../tables/ShortTasksTable";

function AttachedFiles({ attachments, setAttachments }) {
    return (
        <div>
            {isNaN(attachments) && (
                <div>
                    <h4>Прикрепленные файлы:</h4>
                    {attachments.map((attachment, index) => (
                        <div key={index}>
                            <a key={index} href={atob(attachment.data)} download>{attachment.name}</a>
                            <Button
                                onClick={() => {
                                    setAttachments(attachments.filter(item => item !== attachment))
                                }}
                            >x</Button>
                        </div>
                    ))}
                </div>)
            }
        </div>
    )
}

function DropDownMenu({ data, setData, getData, propName, values }) {
    const [isDataLoaded, setIsDataLoaded] = useState(false);

    return (
        <div>
            <Field
                name={propName}
                type="select"
                as={Select}
                onOpen={async () => {
                    setData(await getData())
                    setIsDataLoaded(true);
                }}
            >
                {isDataLoaded
                    ? (data.map(item =>
                        item.lastName == null
                            ? <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                            : <MenuItem key={item.id} value={item.id}>{`${item.firstName} ${item.lastName}`}</MenuItem>
                    ))
                    : (values.lastName == null
                        ? <MenuItem key={values.id} value={values.id}>{values.name}</MenuItem>
                        : <MenuItem key={values.id} value={values.id}>{`${values.firstName} ${values.lastName}`}</MenuItem>
                    )
                }
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
                    id: 0,
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
        </div>
    )
}

function EditTaskForm() {
    const [initialValues, setInitialValues] = useState();
    const [isLoading, setIsLoading] = useState(true);

    const [task, setTask] = useState();
    const [statuses, setStatuses] = useState([]);
    const [authors, setAuthors] = useState([]);
    const [performers, setPerformers] = useState([]);
    const [priorities, setPriorities] = useState([]);
    const [problemAttachments, setProblemAttachments] = useState([]);
    const [responseAttachments, setResponseAttachments] = useState([]);

    const { id } = useParams();
    const history = useHistory();
    const localtion = {
        pathname: "/tasks"
    }

    useEffect(() => {
        const delay = 1.5;

        async function initializeState() {
            setTask(await getById(id));
        }

        initializeState();
        setTimeout(() => {
            setIsLoading(false);
        }, delay * 1000);
    }, [id])

    useEffect(() => {
        setInitialValues(() => {
            let obj = { ...task }

            if (task != null) {
                delete obj.id;
                delete obj.updatedAt;

                function initializeAttachmentsState() {
                    setProblemAttachments(obj.problemAttachments)
                    setResponseAttachments(obj.responseAttachments)
                    obj.problemAttachments = [];
                    obj.responseAttachments = [];
                }

                initializeAttachmentsState();

                return obj;
            }
            else return null;
        });
    }, [task])

    return (
        isLoading
            ? <CircularProgress />
            : (initialValues == null
                ? <div>Соединение с сервером недоступно</div>
                : (
                    <div>
                        <h1>Задача №{task.id}</h1>
                        <Formik
                            initialValues={initialValues}
                            onSubmit={async (data) => {
                                // Данные заносятся не напрямую в форму, т.к. при большом их объеме
                                // Форма будет медленно отображать вносимые изменения
                                data.problemAttachments = problemAttachments;
                                data.responseAttachments = responseAttachments;

                                await update(task.id, data);
                                history.replace(localtion.pathname)
                            }}>

                            {({
                                values, // Текущее состояние формы
                                isSubmitting
                            }) => (
                                <Form>
                                    <div id="summary">
                                        <Field
                                            name="summary"
                                            type="input"
                                            placeholder="Краткое описание"
                                            as={TextField}
                                        />
                                    </div>
                                    <div id="status">
                                        <DropDownMenu
                                            data={statuses}
                                            setData={setStatuses}
                                            getData={getStatuses}
                                            propName={"status.id"}
                                            values={task.status}
                                        />
                                    </div>
                                    <div id="author">
                                        <DropDownMenu
                                            data={authors}
                                            setData={setAuthors}
                                            getData={getEmployees}
                                            propName={"author.id"}
                                            values={task.author}
                                        />
                                    </div>
                                    <div id="performingBy">
                                        <DropDownMenu
                                            data={performers}
                                            setData={setPerformers}
                                            getData={getEmployees}
                                            propName={"performingBy.id"}
                                            values={task.performingBy}
                                        />
                                    </div>
                                    <div id="priority">
                                        <DropDownMenu
                                            data={priorities}
                                            setData={setPriorities}
                                            getData={getPriorities}
                                            propName={"priority.id"}
                                            values={task.priority}
                                        />
                                    </div>
                                    <div id="problemDescription">
                                        <Field
                                            name="problemDescription"
                                            placeholder="Описание задачи"
                                            rowsMin={6}
                                            as={TextareaAutosize} />
                                    </div>
                                    <div id="problemAttachments">
                                        <label htmlFor="problemAttachments"><b>Файлы к задаче</b></label>
                                        <FieldArray name="problemAttachments">
                                            <MyDropzone
                                                attachments={problemAttachments}
                                                setAttachments={setProblemAttachments}
                                            />
                                        </FieldArray>
                                        <AttachedFiles
                                            attachments={problemAttachments}
                                            setAttachments={setProblemAttachments}
                                        />
                                    </div>
                                    <div id="responseDescription">
                                        <Field
                                            name="responseDescription"
                                            placeholder="Комментарий к ответу"
                                            rowsMin={6}
                                            as={TextareaAutosize} />
                                    </div>
                                    <div id="responseAttachments">
                                        <label htmlFor="responseAttachments"><b>Файлы к ответу</b></label>
                                        <FieldArray name="responseAttachments">
                                            <MyDropzone
                                                attachments={responseAttachments}
                                                setAttachments={setResponseAttachments}
                                            />
                                        </FieldArray>
                                        <AttachedFiles
                                            attachments={responseAttachments}
                                            setAttachments={setResponseAttachments}
                                        />
                                    </div>
                                    <div>
                                        <Button
                                            type="submit"
                                            disabled={isSubmitting}
                                        >Изменить</Button>
                                    </div>
                                    <div>
                                        <Button
                                            type="button"
                                            onClick={async() => {
                                                await deleteButtonHandler(task.id, remove)
                                                history.replace(localtion.pathname)
                                            }}
                                            disabled={isSubmitting}
                                        >Удалить</Button>
                                    </div>
                                    {/* <pre>{JSON.stringify(values, null, 2)}</pre> */}
                                </Form>
                            )}
                        </Formik>
                    </div>
                )
            )
    );
}

export default EditTaskForm;