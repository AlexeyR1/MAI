import React, { useState } from "react"
import { Formik, useField, Form } from "formik"
import { Select, MenuItem } from "@material-ui/core";
import { add as addTask } from "../../api/task";
import { DefaultTaskForm } from './DefaultTaskForm';
import { getAll as getStatuses } from "../../api/status";
import { getAll as getPriorities } from "../../api/priority";
import { getAll as getEmployees } from "../../api/shortEmployee";
import * as yup from "yup";

function DropDownMenu({ data, setData, getData, propName, labelName }) {
    const [field, meta] = useField(propName);
    const errorText = meta.error && meta.touched ? meta.error : "";

    return (
        <div>
            <div className="label">{labelName}</div>
            <Select
                {...field}
                error={!!errorText}
                onOpen={async () => {
                    if (propName !== 'status.id') setData(await getData())
                }}
            >
                {data.map(item =>
                    item.lastName == null
                        ? <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                        : <MenuItem key={item.id} value={item.id}>{`${item.firstName} ${item.lastName}`}</MenuItem>
                )}
            </Select>
        </div>
    );
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
        responseDescription: "",
        problemAttachments: []
    });

    const validationSchema = yup.object({
        summary: yup.string().required().max(150),
        status: yup.object({
            id: yup.number().required()
        }),
        author: yup.object({
            id: yup.number().required().notOneOf([0])
        }),
        performingBy: yup.object({
            id: yup.number().required().notOneOf([0])
        }),
        priority: yup.object({
            id: yup.number().required()
        }),
        problemDescription: yup.string().max(2000).nullable()
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
    const [problemAttachments, setProblemAttachments] = useState([])

    return (
        <div className="custom-container">
            <h1>Создание задачи</h1>
            <Formik
                initialValues={initialForm}
                validateOnChange={true}
                validationSchema={validationSchema}
                onSubmit={async (data, { resetForm }) => {
                    // Данные заносятся не напрямую в форму, т.к. при большом их объеме
                    // Форма будет медленно отображать вносимые изменения
                    data.problemAttachments = problemAttachments;

                    await addTask(data);

                    // Очистка состояний
                    resetForm({});
                    setProblemAttachments([]);
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
                            />}
                            Author={
                                <DropDownMenu
                                    data={authors}
                                    setData={setAuthors}
                                    getData={getEmployees}
                                    propName={"author.id"}
                                    labelName="Автор"
                                />}
                            PerformingBy={
                                <DropDownMenu
                                    data={performers}
                                    setData={setPerformers}
                                    getData={getEmployees}
                                    propName={"performingBy.id"}
                                    labelName="Исполнитель"
                                />}
                            Priority={
                                <DropDownMenu
                                    data={priorities}
                                    setData={setPriorities}
                                    getData={getPriorities}
                                    propName={"priority.id"}
                                    labelName="Приоритет"
                                />}
                            problemAttachments={{
                                problemAttachments,
                                setProblemAttachments
                            }}
                        >
                            <div>
                                <button type="submit" disabled={isSubmitting}>Создать</button>
                            </div>
                            {/* <pre>{JSON.stringify(values, null, 2)}</pre> */}
                            {/* <pre>{JSON.stringify(errors, null, 2)}</pre> */}
                        </DefaultTaskForm>
                    </Form>
                )}
            </Formik>
        </div >
    );
}

export default AddTaskForm;