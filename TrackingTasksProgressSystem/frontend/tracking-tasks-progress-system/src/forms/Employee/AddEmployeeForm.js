import React, { useState } from "react";
import { Form, Formik, useField } from "formik";
import { MyTextField } from "../Task/DefaultTaskForm";
import { Select, MenuItem } from "@material-ui/core";
import { getAll } from "../../api/position";
import { add } from "../../api/employee";
import * as yup from "yup";

function DropDownMenu({ ...props }) {
    const [field, meta] = useField(props.propName);
    const errorText = meta.error && meta.touched ? meta.error : "";

    return (
        <div>
            <div className="label">{props.labelName}</div>
            <Select
                {...field}
                error={!!errorText}
                onOpen={async () => {
                    props.setData(await props.getData())
                }}
            >
                {props.data.map(item =>
                    <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                )}
            </Select>
        </div>
    );
}

function AddEmployeeForm() {
    const emailRegex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

    const initialValues = {
        firstName: "",
        lastName: "",
        position: {
            id: 0
        },
        email: ""
    }

    const [positions, setPositions] = useState([]);

    const validationSchema = yup.object({
        firstName: yup.string().required().max(50),
        lastName: yup.string().required().max(50),
        position: yup.object({
            id: yup.number().required().notOneOf([0])
        }),
        email: yup.string()
            .required()
            .max(50)
            .matches(emailRegex, "invalid email format")
    })

    return (
        <div className="custom-container">
            <h1>Добавление сотрудника</h1>
            <Formik
                initialValues={initialValues}
                validateOnChange={true}
                validationSchema={validationSchema}
                onSubmit={async (data, { resetForm }) => {
                    await add(data);
                    resetForm({});
                }}
            >
                {({
                    values,
                    errors,
                    isSubmitting
                }) => (

                    <Form>
                        <div id="firstName">
                            <div className="label">Имя</div>
                            <MyTextField
                                name="firstName"
                                type="input"
                                placeholder="Имя"
                            />
                        </div>
                        <div id="lastName">
                            <div className="label">Фамилия</div>
                            <MyTextField
                                name="lastName"
                                type="input"
                                placeholder="Фамилия"
                            />
                        </div>
                        <div id="position">
                            <DropDownMenu
                                data={positions}
                                setData={setPositions}
                                getData={getAll}
                                propName="position.id"
                                labelName="Должность"
                            />
                        </div>
                        <div id="email">
                            <div className="label">Электронный адрес</div>
                            <MyTextField
                                name="email"
                                type="input"
                                placeholder="Email"
                            />
                        </div>
                        <div>
                            <button type="submit" style={{marginTop: '3%'}} disabled={isSubmitting}>Добавить</button>
                        </div>
                        {/* <pre>{JSON.stringify(values, null, 2)}</pre> */}
                        {/* <pre>{JSON.stringify(errors, null, 2)}</pre> */}
                    </Form>
                )}
            </Formik>
        </div>
    );
}

export default AddEmployeeForm;