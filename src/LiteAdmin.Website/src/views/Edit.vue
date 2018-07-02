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
            <md-button v-on:click="save">Save</md-button>
            <md-button v-on:click="cancel">Cancel</md-button>
        </div>
    </div>
</template>

<script lang="ts" src="./Edit.ts"></script>
