import React, { useState } from "react"
import { Formik, Field, Form } from "formik"
import { Button, TextField, Select, MenuItem, TextareaAutosize } from "@material-ui/core";
import { getAll as getStatuses } from "../api/status";
import { getAll as getPriorities } from "../api/priority";
import { getAll as getEmployees } from "../api/shortEmployee";
// import { date } from "yup";

function DropDownMenu({ data, setData, getData, propName }) {
    return (
        <div>
            <div>{JSON.stringify(data, null, 2)}</div>
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

function AddTaskForm() {
    // Статусы
    const [statuses, setStatuses] = useState([{
        id: 1,
        name: "К выполнению"
    }]);

    // Авторы
    const [authors, setAuthors] = useState([{
        id: 0,
        firstName: "",
        lastName: ""
    }]);

    // Исполнители
    const [performers, setPerformers] = useState([{
        id: 0,
        firstName: "",
        lastName: ""
    }]);

    // Приоритеты
    const [priorities, setPriorities] = useState([{
        id: 2,
        name: "Средний"
    }]);

    return (
        <div>
            <Formik
                initialValues={{
                    summary: '',
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
                    problemAnnotation: '',
                    problemAttachments: [
                        // {
                        //     name: '',
                        //     data: []
                        // }
                    ]
                }}
                // Вызывается при отправке формы
                onSubmit={(data, { setSubmitting, resetForm }) => {
                    setSubmitting(true);
                    // make async call
                    console.log("submit: ", data);
                    resetForm({});
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
                        <div>
                            <Field />
                        </div>
                        <div>
                            <Button type="submit" disabled={isSubmitting}>submit</Button>
                        </div>
                        <pre>{JSON.stringify(values, null, 2)}</pre>
                    </Form>
                )}
            </Formik>
        </div>
    );
}

export default AddTaskForm;