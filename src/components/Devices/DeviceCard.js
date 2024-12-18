import React from 'react';
import './DeviceCard.css'; // Импортируем стили

const DeviceCard = ({ device, onEdit, onDelete }) => {
    const schedule = "12:00 - 15:00";

    return (
        <div className="device-card">
            <h3 className="device-card-title">{device.name}</h3>
            <p className="device-card-status">Status: {device.status}</p>
            <div className="device-card-content">
            <div className="flex justify-between mt-2">
                <button
                    className="device-card-button"
                    onClick={() => onEdit(device)}
                >
                    Edit
                </button>
                <button
                    className="device-card-button device-card-button-delete"
                    onClick={() => onDelete(device.id)}
                >
                    Delete
                </button>
            </div>
            <div className="schedule-container">
                <h2 className="device-card-title">Schedule</h2>
                <p className="device-card-schedule">{schedule}</p>
            </div>
            </div>
        </div>
    );
};

export default DeviceCard;