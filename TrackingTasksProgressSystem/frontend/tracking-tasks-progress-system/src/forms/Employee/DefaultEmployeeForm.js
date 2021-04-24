import React from "react";
import { Form } from "formik"
import { MyTextField } from "../TextField";
import * as yup from "yup";

export function validationSchema() {
    const emailRegex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

    return (
        yup.object({
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
    );
}

export function DefaultEmployeeForm({ Position, Department, children }) {
    return (
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
            <div id="position">{Position}</div>
            <div id="department">{Department}</div>
            <div id="email">
                <div className="label">Электронный адрес</div>
                <MyTextField
                    name="email"
                    type="input"
                    placeholder="Email"
                />
            </div>
            {children}
        </Form>
    )
}