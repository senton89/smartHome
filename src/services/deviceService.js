import axios from 'axios';

const API_URL = 'http://localhost:5202/api/devices';

const createDevice = async (deviceData) => {
    const response = await axios.post(API_URL, deviceData);
    return response.data;
};

const deleteDevice = async (deviceId) => {
    await axios.delete(`${API_URL}/${deviceId}`);
};

const getDevices = async (filter) => {
    const response = await axios.get(API_URL, { params: filter });
    return response.data;
};

const getDevice = async (deviceId) => {
    const response = await axios.get(`${API_URL}/${deviceId}`);
    return response.data;
};

const updateDevice = async (deviceId, deviceData) => {
    await axios.patch(`${API_URL}/${deviceId}`, deviceData);
};

const connectSchedules = async (deviceId, schedules) => {
    await axios.post(`${API_URL}/${deviceId}/schedules`, schedules);
};

const disconnectSchedules = async (deviceId, schedules) => {
    await axios.delete(`${API_URL}/${deviceId}/schedules`, { data: schedules });
};

const findSchedules = async (deviceId, filter) => {
    const response = await axios.get(`${API_URL}/${deviceId}/schedules`, { params: filter });
    return response.data;
};

export {
    createDevice,
    deleteDevice,
    getDevices,
    getDevice,
    updateDevice,
    connectSchedules,
    disconnectSchedules,
    findSchedules,
};