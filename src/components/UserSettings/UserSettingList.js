import React, { useEffect, useState } from 'react';
import { getUserSettings, deleteUserSetting } from '../../services/userSettingService';

const UserSettingList = ({ onEdit }) => {
    const [userSettings, setUserSettings] = useState([]);

    useEffect(() => {
        const fetchUserSettings = async () => {
            const data = await getUserSettings({});
            setUserSettings(data);
        };
        fetchUserSettings();
    }, []);

    const handleDelete = async (userSettingId) => {
        await deleteUserSetting(userSettingId);
        setUserSettings(userSettings.filter(setting => setting.id !== userSettingId));
    };

    return (
        <div>
            <h2>User Settings</h2>
            <ul>
                {userSettings.map(setting => (
                    <li key={setting.id}>
                        Theme: {setting.theme}, Notifications: {setting.notificationsEnabled ? 'Enabled' : 'Disabled'}
                        <button onClick={() => onEdit(setting)}>Edit</button>
                        <button onClick={() => handleDelete(setting.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default UserSettingList;