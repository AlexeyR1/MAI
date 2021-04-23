import React, { useState, useEffect } from "react";
import { Formik, Field, Form, FieldArray } from "formik"
import { useParams, useHistory } from "react-router-dom";
import { Select, MenuItem } from "@material-ui/core";
import CircularProgress from '@material-ui/core/CircularProgress';
import { getAll as getStatuses } from "../../api/status";
import { getAll as getPriorities } from "../../api/priority";
import { getAll as getEmployees } from "../../api/shortEmployee";
import { getById, update, remove } from "../../api/task";
import { deleteButtonHandler } from "../../tables/ShortTasksTable";
import { AttachedFiles, MyDropzone, DefaultTaskForm, MyTextArea } from "./DefaultTaskForm";
import * as yup from "yup";

function DropDownMenu({ data, setData, getData, propName, labelName, values, isDisabled }) {
    const [isDataLoaded, setIsDataLoaded] = useState(false);

    return (
        <div>
            <div className="label">{labelName}</div>
            <Field
                name={propName}
                type="select"
                as={Select}
                disabled={isDisabled}
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

    const validationSchema = yup.object({
        summary: yup.string().required().max(150),
        status: yup.object({
            id: yup.number().required()
        }),
        author: yup.object({
            id: yup.number().required()
        }),
        performingBy: yup.object({
            id: yup.number().required()
        }),
        priority: yup.object({
            id: yup.number().required()
        }),
        problemDescription: yup.string().max(2000).nullable(),
        responseDescription: yup.string().max(2000).nullable(),
    });

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
        async function initializeState() {
            let result = await getById(id);
            if (!result) setIsLoading(false)
            else {
                setTask(result)
                setIsLoading(false)
            }
        }

        initializeState();
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
                            <h1 style={{ marginBottom: '0' }}>Задача №{task.id}</h1>
                            <div style={{ fontSize: '80%' }}>Обновлено: {task.updatedAt}</div>
                            <Formik
                                initialValues={initialValues}
                                validateOnChange={true}
                                validationSchema={validationSchema}
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
                                    errors,
                                    isSubmitting
                                }) => (
                                    <Form>
                                        <DefaultTaskForm
                                            Status={<DropDownMenu
                                                data={statuses}
                                                setData={setStatuses}
                                                getData={getStatuses}
                                                propName={"status.id"}
                                                labelName="Статус"
                                                values={values.status}
                                                isDisabled={false}
                                            />}
                                            Author={
                                                <DropDownMenu
                                                    data={authors}
                                                    setData={setAuthors}
                                                    getData={getEmployees}
                                                    propName={"author.id"}
                                                    labelName="Автор"
                                                    values={values.author}
                                                    isDisabled={true}
                                                />}
                                            PerformingBy={
                                                <DropDownMenu
                                                    data={performers}
                                                    setData={setPerformers}
                                                    getData={getEmployees}
                                                    propName={"performingBy.id"}
                                                    labelName="Исполнитель"
                                                    values={values.performingBy}
                                                    isDisabled={false}
                                                />}
                                            Priority={
                                                <DropDownMenu
                                                    data={priorities}
                                                    setData={setPriorities}
                                                    getData={getPriorities}
                                                    propName={"priority.id"}
                                                    labelName="Приоритет"
                                                    values={values.priority}
                                                    isDisabled={false}
                                                />}
                                            problemAttachments={{
                                                problemAttachments,
                                                setProblemAttachments
                                            }}
                                        >
                                            <div id="responseDescription">
                                                <div className="label">Комментарий к ответу</div>
                                                <MyTextArea
                                                    name="responseDescription"
                                                    placeholder="Введите текст"
                                                />
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
                                                        if (await deleteButtonHandler(task.id, remove)) {
                                                            history.replace(localtion.pathname)
                                                        }
                                                    }}
                                                    disabled={isSubmitting}
                                                >Удалить</button>
                                            </div>
                                            {/* <pre>{JSON.stringify(values, null, 2)}</pre> */}
                                            {/* <pre>{JSON.stringify(errors, null, 2)}</pre> */}
                                        </DefaultTaskForm>
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