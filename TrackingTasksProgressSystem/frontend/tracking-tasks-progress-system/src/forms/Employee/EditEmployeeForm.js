import React, { useState, useEffect } from "react";
import { useField, Formik } from "formik";
import { useParams, useHistory } from "react-router-dom";
import { Select, MenuItem } from "@material-ui/core";
import CircularProgress from '@material-ui/core/CircularProgress';
import { getAll } from "../../api/position";
import { getById, update } from "../../api/employee";
import { validationSchema, DefaultEmployeeForm } from "./DefaultEmployeeForm";

function DropDownMenu({ values, ...props }) {
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
                    props.setIsDataLoaded(true);
                }}
            >
                {props.isDataLoaded
                    ? props.data.map(item =>
                        <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                    )
                    : <MenuItem key={values.position.id} value={values.position.id}>{values.position.name}</MenuItem>
                }
            </Select>
        </div >
    );
}

function EditEmployeeForm() {
    const [initialValues, setInitialValues] = useState();
    const [isLoading, setIsLoading] = useState(true);

    const { id } = useParams();
    const history = useHistory();
    const localtion = { pathname: "/tasks" }

    const [employee, setEmployee] = useState();
    const [positions, setPositions] = useState([]);
    const [isDataLoaded, setIsDataLoaded] = useState(false);

    useEffect(() => {
        async function initializeState() {
            let result = await getById(id);
            if (!result) setIsLoading(false)
            else {
                setEmployee(result)
                setIsLoading(false)
            }
        }

        initializeState();
    }, [id])

    useEffect(() => {
        setInitialValues(() => {
            let obj = { ...employee }

            if (employee != null) {
                delete obj.id;

                return obj;
            }
            else return null;
        });
    }, [employee])

    return (
        <div className="custom-container">
            {isLoading
                ? <CircularProgress />
                : (<React.Fragment><h1>Cотрудник №{employee.id}</h1>
                    <Formik
                        initialValues={initialValues}
                        validateOnChange={true}
                        validationSchema={validationSchema}
                        onSubmit={async (data) => {
                            await update(employee.id, data)
                            history.replace(localtion.pathname)
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
                                        values={values}
                                        isDataLoaded={isDataLoaded}
                                        setIsDataLoaded={setIsDataLoaded}
                                    />
                                }
                                Department={
                                    <React.Fragment>
                                        <div className="label">Отдел</div>
                                        <Select value={values.position.id} disabled>
                                            {isDataLoaded
                                                ? (positions.map(item =>
                                                    <MenuItem key={item.id} value={item.id}>{item.department.name}</MenuItem>
                                                ))
                                                : <MenuItem value={values.position.id}>{values.position.department.name}</MenuItem>
                                            }
                                        </Select>
                                    </React.Fragment>
                                }
                            >
                                <div>
                                    <button type="submit" style={{ marginTop: '3%' }} disabled={isSubmitting}>Изменить</button>
                                </div>
                                {/* <pre>{JSON.stringify(values, null, 2)}</pre> */}
                                {/* <pre>{JSON.stringify(errors, null, 2)}</pre> */}
                            </DefaultEmployeeForm>
                        )}
                    </Formik>
                </React.Fragment>
                )
            }
        </div >
    );
}

export default EditEmployeeForm;