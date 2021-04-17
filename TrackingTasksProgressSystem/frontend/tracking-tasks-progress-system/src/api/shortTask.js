import axios from "axios"
import config from "./config";

const instance = axios.create({
    baseURL: `${config.BASE_URL}/shorttask`
});

export const getAll = () => instance.get("").then(result => result.data);

export const getById = (id) => instance.get(`${id}`).then(result => result.data);