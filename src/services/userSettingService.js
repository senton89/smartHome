import axios from 'axios';

const API_URL = 'http://localhost:5202/api/usersettings';

const createUserSetting = async (userSettingData) => {
    const response = await axios.post(API_URL, userSettingData);
    return response.data;
};

const deleteUserSetting = async (userSettingId) => {
    await axios.delete(`${API_URL}/${userSettingId}`);
};

const getUserSettings = async (filter) => {
    const response = await axios.get(API_URL, { params: filter });
    return response.data;
};

const getUserSetting = async (userSettingId) => {
    const response = await axios.get(`${API_URL}/${userSettingId}`);
    return response.data;
};

const updateUserSetting = async (userSettingId, userSettingData) => {
    await axios.patch(`${API_URL}/${userSettingId}`, userSettingData);
};

export {
    createUserSetting,
    deleteUserSetting,
    getUserSettings,
    getUserSetting,
    updateUserSetting,
};