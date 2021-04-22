import React, { useCallback } from "react"
import { FieldArray, useField } from "formik"
import { useDropzone } from "react-dropzone";
import { TextField, TextareaAutosize, FormHelperText, FormControl } from "@material-ui/core";

export function AttachedFiles({ attachments, setAttachments }) {
    return (
        <div className="attachment-container">
            {isNaN(attachments) && (
                <React.Fragment>
                    <div className="label">Прикрепленные файлы</div>
                    <div>
                        {attachments.map((attachment, index) => (
                            <div key={index}>
                                <a key={index} href={atob(attachment.data)} style={{ marginRight: '2%' }} download>{attachment.name}</a>
                                <button
                                    type="button"
                                    className="muted-button"
                                    onClick={() => {
                                        setAttachments(attachments.filter(item => item !== attachment))
                                    }}
                                >x</button>
                            </div>
                        ))}
                    </div>
                </React.Fragment>
            )}
        </div>
    )
}

export function MyDropzone({ attachments, setAttachments }) {
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
            <div className="dropzone" {...getRootProps()}>
                <input {...getInputProps()} />
                <p>&nbsp;&nbsp;Поместите сюда Ваши файлы</p>
            </div>
        </div>
    )
}

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

export const MyTextArea = ({ placeholder, ...props }) => {
    const [field, meta] = useField(props);
    const errorText = meta.error && meta.touched ? meta.error : "";

    return (
        <FormControl style={{width: '100%'}}>
            <TextareaAutosize
                {...field}
                placeholder={placeholder}
                rowsMin={6}
            />
            <div><FormHelperText error={!!errorText}>{errorText}</FormHelperText></div>
        </FormControl>
    )
}

export function DefaultForm({ Status, Author, PerformingBy, Priority, problemAttachments, children }) {
    return (
        <div>
            <div id="summary">
                <div className="label">Краткое описание</div>
                <MyTextField
                    name="summary"
                    type="input"
                    placeholder="Введите текст"
                />
            </div>
            <div id="status">{Status}</div>
            <div id="author">{Author}</div>
            <div id="performingBy">{PerformingBy}</div>
            <div id="priority">{Priority}</div>
            <div id="problemDescription">
                <div className="label">Описание задачи</div>
                <MyTextArea
                    name="problemDescription"
                    placeholder="Введите текст"
                />
            </div>
            <div id="problemAttachments">
                <div className="label">Файлы к задаче</div>
                <FieldArray name="problemAttachments">
                    <MyDropzone
                        attachments={problemAttachments.problemAttachments}
                        setAttachments={problemAttachments.setProblemAttachments}
                    />
                </FieldArray>
                <AttachedFiles
                    attachments={problemAttachments.problemAttachments}
                    setAttachments={problemAttachments.setProblemAttachments}
                />
            </div>
            {children}
        </div>
    );
}