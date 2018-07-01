<template>
    <div class="edit">
        <h1>Edit {{getFriendlyName(tableName)}}</h1>
        <div v-for="(column, index) in tableSchema.columns" class="form__group">
            <label :for="column.name" class="form__label">{{getFriendlyName(column.name)}}</label>

            <input :id="column.name"
                   :name="column.name"
                   :ref="column.name"
                   :type="getInputType(column.dataType)"
                   v-model="item[column.name]"
                   :maxlength="getMaxLength(column.maxLength)"
                   class="form__control"
                   :disabled="column.isPrimaryKey"/>
        </div>

        <div class="button__group">
            <button type="button" v-on:click="save" class="button button__primary">Save</button>
            <button type="button" v-on:click="cancel" class="button button--grey">Cancel</button>
        </div>
    </div>
</template>

<script lang="ts" src="./Edit.ts"></script>
