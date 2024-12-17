import React, { useState } from 'react';
import { createUserSetting, updateUserSetting } from '../../services/userSettingService';

const UserSettingForm = ({ userSetting, onSuccess }) => {
    const [theme, setTheme] = useState(userSetting ? userSetting.theme : '');
    const [notificationsEnabled, setNotificationsEnabled] = useState(userSetting ? userSetting.notificationsEnabled : false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const userSettingData = { theme, notificationsEnabled };
        if (userSetting) {
            await updateUserSetting(userSetting.id, userSettingData);
        } else {
            await createUserSetting(userSettingData);
        }
        onSuccess();
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={theme}
                onChange={(e) => setTheme(e.target.value)}
                placeholder="Theme"
                required
            />
            <label>
                Notifications Enabled:
                <input
                    type="checkbox"
                    checked={notificationsEnabled}
                    onChange={(e) => setNotificationsEnabled(e.target.checked)}
                />
            </label>
            <button type="submit">{userSetting ? 'Update' : 'Create'} User Setting</button>
        </form>
    );
};

export default UserSettingForm;