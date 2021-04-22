import React, { useState, useEffect } from "react";
import { Formik, Field, Form, FieldArray } from "formik"
import { useParams, useHistory } from "react-router-dom";
import { Select, MenuItem, TextareaAutosize } from "@material-ui/core";
import CircularProgress from '@material-ui/core/CircularProgress';
import { getAll as getStatuses } from "../api/status";
import { getAll as getPriorities } from "../api/priority";
import { getAll as getEmployees } from "../api/shortEmployee";
import { getById, update, remove } from "../api/task";
import { deleteButtonHandler } from "../tables/ShortTasksTable";
import { AttachedFiles, MyDropzone, DefaultForm } from "./DefaultForm";

function DropDownMenu({ data, setData, getData, propName, labelName, values }) {
    const [isDataLoaded, setIsDataLoaded] = useState(false);

    return (
        <div>
            <div className="label">{labelName}</div>
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

function EditTaskForm() {
    const [initialValues, setInitialValues] = useState();
    const [isLoading, setIsLoading] = useState(true);

    const [task, setTask] = useState();
    const [statuses, setStatuses] = useState();
    const [authors, setAuthors] = useState();
    const [performers, setPerformers] = useState();
    const [priorities, setPriorities] = useState();
    const [problemAttachments, setProblemAttachments] = useState(null);
    const [responseAttachments, setResponseAttachments] = useState(null);

    const { id } = useParams();
    const history = useHistory();
    const localtion = { pathname: "/tasks" }

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
                function initializeAttachmentStates() {
                    setProblemAttachments(obj.problemAttachments)
                    setResponseAttachments(obj.responseAttachments)
                    obj.problemAttachments = [];
                    obj.responseAttachments = [];
                }

                delete obj.id;
                delete obj.updatedAt;
                initializeAttachmentStates();

                return obj;
            }
            else return null;
        });
    }, [task])

    return (
        <div className="custom-container">
            {isLoading
                ? <CircularProgress style={{ marginTop: '5%' }} />
                : (initialValues == null
                    ? <div>Соединение с сервером недоступно</div>
                    : (
                        <React.Fragment>
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
                                        <DefaultForm
                                            Status={<DropDownMenu
                                                data={statuses}
                                                setData={setStatuses}
                                                getData={getStatuses}
                                                propName={"status.id"}
                                                labelName="Статус"
                                                values={values.status}
                                            />}
                                            Author={
                                                <DropDownMenu
                                                    data={authors}
                                                    setData={setAuthors}
                                                    getData={getEmployees}
                                                    propName={"author.id"}
                                                    labelName="Автор"
                                                    values={values.author}
                                                />}
                                            PerformingBy={
                                                <DropDownMenu
                                                    data={performers}
                                                    setData={setPerformers}
                                                    getData={getEmployees}
                                                    propName={"performingBy.id"}
                                                    labelName="Исполнитель"
                                                    values={values.performingBy}
                                                />}
                                            Priority={
                                                <DropDownMenu
                                                    data={priorities}
                                                    setData={setPriorities}
                                                    getData={getPriorities}
                                                    propName={"priority.id"}
                                                    labelName="Приоритет"
                                                    values={values.priority}
                                                />}
                                            problemAttachments={{
                                                problemAttachments,
                                                setProblemAttachments
                                            }}
                                        >
                                            <div id="responseDescription">
                                                <div className="label">Комментарий к ответу</div>
                                                <Field
                                                    name="responseDescription"
                                                    placeholder="Комментарий к ответу"
                                                    rowsMin={6}
                                                    as={TextareaAutosize} />
                                            </div>
                                            <div id="responseAttachments">
                                                <div className="label">Файлы к ответу</div>
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
                                            <div className="inline-block">
                                                <button type="submit" disabled={isSubmitting}>Изменить</button>
                                            </div>
                                            <div className="inline-block" style={{ marginLeft: '48%' }}>
                                                <button
                                                    className="accent-button"
                                                    type="button"
                                                    onClick={async () => {
                                                        await deleteButtonHandler(task.id, remove)
                                                        history.replace(localtion.pathname)
                                                    }}
                                                    disabled={isSubmitting}
                                                >Удалить</button>
                                            </div>
                                            {/* <pre>{JSON.stringify(values, null, 2)}</pre> */}
                                        </DefaultForm>
                                    </Form>
                                )}
                            </Formik>
                        </React.Fragment>
                    )
                )}
        </div>
    );
}

export default EditTaskForm;