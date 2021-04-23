import React from "react";
import { useField } from "formik";
import { TextField } from "@material-ui/core";

export const MyTextField = ({ placeholder, ...props }) => {
    const [field, meta] = useField(props);
    const errorText = meta.error && meta.touched ? meta.error : "";

    return (
        <TextField
            {...field}
            placeholder={placeholder}
            variant="outlined"
            fullWidth
            size="small"
            helperText={errorText}
            error={!!errorText}
        />
    )
}