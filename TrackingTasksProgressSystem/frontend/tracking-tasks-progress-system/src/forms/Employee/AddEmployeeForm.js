import React, { useState } from "react";
import { Formik, useField } from "formik";
import { Select, MenuItem } from "@material-ui/core";
import { getAll } from "../../api/position";
import { add } from "../../api/employee";
import { validationSchema, DefaultEmployeeForm } from "./DefaultEmployeeForm";

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
    const initialValues = {
        firstName: "",
        lastName: "",
        position: {
            id: 0
        },
        email: ""
    }

    const [positions, setPositions] = useState([]);

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
                    <DefaultEmployeeForm
                        Position={
                            <DropDownMenu
                                data={positions}
                                setData={setPositions}
                                getData={getAll}
                                propName="position.id"
                                labelName="Должность"
                            />
                        }
                        Department={
                            <React.Fragment>
                                <div className="label">Отдел</div>
                                <Select value={values.position.id} disabled>
                                    {positions.map(item =>
                                        <MenuItem key={item.id} value={item.id}>{item.department.name}</MenuItem>
                                    )}
                                </Select>
                            </React.Fragment>
                        }
                    >
                        <div>
                            <button type="submit" style={{ marginTop: '3%' }} disabled={isSubmitting}>Добавить</button>
                        </div>
                        {/* <pre>{JSON.stringify(values, null, 2)}</pre> */}
                        {/* <pre>{JSON.stringify(errors, null, 2)}</pre> */}
                    </DefaultEmployeeForm>
                )}
            </Formik>
        </div>
    );
}

export default AddEmployeeForm;