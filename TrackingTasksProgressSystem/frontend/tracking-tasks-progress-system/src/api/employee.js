import axios from "axios"
import config from "./config";

const instance = axios.create({
    baseURL: `${config.BASE_URL}/employee`
});

export const getAll = () => instance.get("").then(result => result.data);

export const getById = (id) => instance.get(`${id}`).then(result => result.data);

export const add = (item) => instance.post("", item).then(result => result.data);

export const update = (item) => instance.put(`${id}`, item).then(result => result.data);