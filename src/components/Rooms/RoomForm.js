import React, { useState } from 'react';
import { createRoom, updateRoom } from '../../services/roomService';
import './RoomForm.css'; // Импортируем стили

const RoomForm = ({ room, onSuccess }) => {
    const [name, setName] = useState(room ? room.name : '');
    const [floor, setFloor] = useState(room ? room.floor : '');

    const handleSubmit = async (e) => {
        e.preventDefault();
        const roomData = { name, floor };
        if (room) {
            await updateRoom(room.id, roomData);
        } else {
            await createRoom(roomData);
        }
        onSuccess();
    };

    return (
        <div className="room-form-container">
            <h2 className="room-form-title">{room ? 'Update Room' : 'Create Room'}</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    placeholder="Room Name"
                    required
                    className="room-form-input"
                />
                <input
                    type="number"
                    value={floor}
                    onChange={(e) => setFloor(e.target.value)}
                    placeholder="Floor"
                    required
                    className="room-form-input"
                />
                <button type="submit" className="room-form-button">
                    {room ? 'Update' : 'Create'} Room
                </button>
            </form>
        </div>
    );
};

export default RoomForm;